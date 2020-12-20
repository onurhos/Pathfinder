using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;
using Pathfinder.Application.Services.Plateau;
using Pathfinder.Commands;
using Pathfinder.Domain;
using PathFinder.Queries;
using Pathfinder.ReadModel.InMemory;

namespace Pathfinder.Application
{
    public class ApplicationModule : IModule
    {
        public static Assembly Assembly { get; } = typeof(ApplicationModule).Assembly;
        
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .AddDefaults(Assembly)
                .AddCommandHandlers()
                .RegisterModule<DomainModule>()
                .RegisterModule<CommandsModule>()
                .RegisterModule<QueriesModule>()
                .RegisterModule<InMemoryReadModelModule>()
                .RegisterServices(serviceRegistration =>
                {
                    serviceRegistration.Register<IPathfinderService, PathfinderService>();
                });
        }
    }
}