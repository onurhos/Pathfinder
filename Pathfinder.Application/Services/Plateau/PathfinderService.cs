using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EventFlow;
using EventFlow.Queries;
using Pathfinder.Commands.Plateau;
using Pathfinder.Domain.Aggregates.Plateau;
using Pathfinder.Domain.Aggregates.Plateau.Entities;
using Pathfinder.Domain.Aggregates.Plateau.Types;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;
using PathFinder.Queries.Plateau;
using Pathfinder.ReadModel.Plateau;

namespace Pathfinder.Application.Services.Plateau
{
    public class PathfinderService : IPathfinderService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public PathfinderService(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        public async Task<Result<PlateauId, ServiceError>> CreatePlateauAsync(Boundary boundary)
        {
            var id = PlateauId.New;
            var command = new CreatePlateauCommand(id, boundary);
            var executionResult = await _commandBus.PublishAsync(command, CancellationToken.None);
            return ErrorProvider.MapExecutionResultError(executionResult, id);
        }

        public async Task<Maybe<ServiceError>> DeployRoverAsync(PlateauId plateauId, RoverId roverId, Position position, Direction direction)
        {
            var rover = new Rover(roverId, position, direction);
            var command = new DeployRoverCommand(plateauId, rover);
            var executionResult = await _commandBus.PublishAsync(command, CancellationToken.None);
            return ErrorProvider.MapExecutionResultErrorMaybe(executionResult);
        }

        public async Task<Maybe<ServiceError>> RotateRoverAsync(PlateauId plateauId, RoverId roverId, Rotate rotate)
        {
            var command = new RotateRoverCommand(plateauId, roverId, rotate);
            var executionResult = await _commandBus.PublishAsync(command, CancellationToken.None);
            return ErrorProvider.MapExecutionResultErrorMaybe(executionResult);
        }

        public async Task<Maybe<ServiceError>> MoveRoverAsync(PlateauId plateauId, RoverId roverId)
        {
            var command = new MoveRoverCommand(plateauId, roverId);
            var executionResult = await _commandBus.PublishAsync(command, CancellationToken.None);
            return ErrorProvider.MapExecutionResultErrorMaybe(executionResult);
        }

        public async Task<Maybe<PlateauReadModel>> GetPlateauStateAsync(PlateauId plateauId)
        {
            var query = new GetPlateauQuery(plateauId.Value);
            var results = await _queryProcessor.ProcessAsync(query, CancellationToken.None);
            return results;
        }
    }
}