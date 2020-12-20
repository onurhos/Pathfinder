using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.Extensions;
using Pathfinder.Application;
using Pathfinder.Application.Services.Plateau;
using Pathfinder.Domain.Aggregates.Plateau;
using Pathfinder.Domain.Aggregates.Plateau.Entities;
using Pathfinder.Domain.Aggregates.Plateau.Events;
using Pathfinder.Domain.Aggregates.Plateau.Types;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;
using Serilog;

namespace Pathfinder.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var pathfinderService = serviceProvider.GetService<IPathfinderService>();
            if (pathfinderService == null)
            {
                Console.WriteLine("Could not initialize pathfinder service.");
                return;
            }

            //  Create plateau
            var plateauBoundary = new Boundary(5, 5);
            var plateauCreateResult = await pathfinderService.CreatePlateauAsync(plateauBoundary);
            if (plateauCreateResult.IsFailure)
            {
                plateauCreateResult.Error.PrintToConsole();
                return;
            }
            
            var plateauId = plateauCreateResult.Value;
            
            //  Deploy first rover
            var roverId1 = RoverId.New;
            var rover1DeployResult = await pathfinderService.DeployRoverAsync(plateauId, roverId1, new Position(1, 2), Direction.N);
            if (rover1DeployResult.HasValue)
            {
                Console.WriteLine("Rover 1 could not be deployed:");
                rover1DeployResult.Value.PrintToConsole();
                return;
            }

            //  Deploy second rover
            var roverId2 = RoverId.New;
            var rover2DeployResult = await pathfinderService.DeployRoverAsync(plateauId, roverId2, new Position(3, 3), Direction.E);
            if (rover2DeployResult.HasValue)
            {
                Console.WriteLine("Rover 2 could not be deployed:");
                rover1DeployResult.Value.PrintToConsole();
                return;
            }

            //  Execute instructions
            Console.WriteLine();
            Console.WriteLine("Executing instructions");
            Console.WriteLine("======================");
            
            var rover1InstructionsExecuted = await ExecuteInstructions(pathfinderService, plateauId, roverId1, "LMLMLMLMM");
            if (!rover1InstructionsExecuted) return;
            
            Console.WriteLine();
            
            var rover2InstructionsExecuted = await ExecuteInstructions(pathfinderService, plateauId, roverId2, "MMRMMRMRRM");
            if (!rover2InstructionsExecuted) return;

            //  Query and print final state
            Console.WriteLine();
            Console.WriteLine("Final positions");
            Console.WriteLine("===============");
            var queryStateOk = await QueryState(pathfinderService, plateauId);
            if (!queryStateOk) return;

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static async Task<bool> QueryState(IPathfinderService pathfinderService, PlateauId plateauId)
        {
            var maybePlateauState = await pathfinderService.GetPlateauStateAsync(plateauId);
            if (maybePlateauState.HasNoValue)
            {
                Console.WriteLine("Could not query the plate state");
                return false;
            }

            var plateauState = maybePlateauState.Value;

            var rover1 = plateauState.Rovers.First();
            Console.Write($"{rover1.Id}: ");
            rover1.PrintToConsole();

            var rover2 = plateauState.Rovers.Last();
            Console.Write($"{rover2.Id}: ");
            rover2.PrintToConsole();
            return true;
        }

        private static async Task<bool> ExecuteInstructions(IPathfinderService pathfinderService, PlateauId plateauId, RoverId roverId, string commands)
        {
            foreach (var command in commands)
            {
                var serviceError = command switch
                {
                    'L' => await pathfinderService.RotateRoverAsync(plateauId, roverId, Rotate.L),
                    'R' => await pathfinderService.RotateRoverAsync(plateauId, roverId, Rotate.R),
                    'M' => await pathfinderService.MoveRoverAsync(plateauId, roverId),
                    _ => null
                };
                if (serviceError == null || serviceError.HasNoValue) continue;
                serviceError.Value.PrintToConsole();
                return false;
            }

            return true;
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Error()
                .CreateLogger();

            Log.Logger = logger;

            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.Populate(services);

            var container = EventFlowOptions.New
                .UseAutofacContainerBuilder(containerBuilder)
                .RegisterModule<ApplicationModule>()
                .AddSynchronousSubscriber<Plateau, PlateauId, PlateauCreatedEvent, EventSubscriber>()
                .AddSynchronousSubscriber<Plateau, PlateauId, RoverDeployedEvent, EventSubscriber>()
                .AddSynchronousSubscriber<Plateau, PlateauId, RoverDirectionChangedEvent, EventSubscriber>()
                .AddSynchronousSubscriber<Plateau, PlateauId, RoverMovedEvent, EventSubscriber>()
                .UseLibLog(LibLogProviders.Serilog)
                .CreateContainer();


            return new AutofacServiceProvider(container);
        }
    }
}
