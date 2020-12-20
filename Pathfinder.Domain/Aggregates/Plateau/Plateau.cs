using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Extensions;
using EventFlow.Provided.Specifications;
using Pathfinder.Domain.Aggregates.Plateau.Entities;
using Pathfinder.Domain.Aggregates.Plateau.Events;
using Pathfinder.Domain.Aggregates.Plateau.Specifications;
using Pathfinder.Domain.Aggregates.Plateau.Types;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;

namespace Pathfinder.Domain.Aggregates.Plateau
{
    public sealed class Plateau : AggregateRootBase<Plateau, PlateauId>
    {
        private readonly PlateauState _state = new PlateauState();

        public Boundary Boundary => _state.Boundary;
        public IReadOnlyList<Rover> Rovers => _state.Rovers;

        public Plateau(PlateauId aggregateId) : base(aggregateId)
        {
            Register(_state);
        }

        public Task<IExecutionResult> CreatePlateau(Boundary boundary)
        {
            return ExecuteIfSpecificationsValid(
                () =>
                {
                    Emit(new PlateauCreatedEvent(boundary));
                }
                , () => new AggregateIsNewSpecification().IsNotSatisfiedByAsExecutionResult(this)
                , () => new PlateauBoundaryValidationSpecification().IsNotSatisfiedByAsExecutionResult(boundary)
            );
        }

        public Task<IExecutionResult> DeployRover(Rover rover)
        {
            return ExecuteIfSpecificationsValid(
                () =>
                {
                    Emit(new RoverDeployedEvent(rover));
                }
                , () => new RoverPositionSpecification().IsNotSatisfiedByAsExecutionResult(rover)
            );
        }

        public Task<IExecutionResult> RotateRover(RoverId roverId, Rotate rotate)
        {
            var rover = Rovers.FirstOrDefault(x => x.Id == roverId);
            if (rover == null)
                return Task.FromResult(ExecutionResult.Failed($"Could not found the rover {roverId}"));

            switch (rotate)
            {
                case Rotate.L:
                    {
                        var newDirectionAsNumber = (int)rover.Direction - 1;
                        if (newDirectionAsNumber < (int)Direction.N) newDirectionAsNumber = (int)Direction.W;
                        Emit(new RoverDirectionChangedEvent(roverId.Value, (Direction)newDirectionAsNumber));
                        return Task.FromResult(ExecutionResult.Success());
                    }
                case Rotate.R:
                    {
                        var newDirectionAsNumber = (int)rover.Direction + 1;
                        if (newDirectionAsNumber > (int)Direction.W) newDirectionAsNumber = (int)Direction.N;
                        Emit(new RoverDirectionChangedEvent(roverId.Value, (Direction)newDirectionAsNumber));
                        return Task.FromResult(ExecutionResult.Success());
                    }
                default:
                    return Task.FromResult(ExecutionResult.Failed("Invalid rotation command"));
            }
        }

        public Task<IExecutionResult> MoveRover(RoverId roverId)
        {
            var rover = Rovers.FirstOrDefault(x => x.Id == roverId);
            if (rover == null)
                return Task.FromResult(ExecutionResult.Failed($"Could not found the rover {roverId}"));

            var newPosition = rover.Position.MoveTo(rover.Direction, Boundary);
            
            //  Overlapped with other rovers?
            var otherRoversExistsInNewLocation = Rovers.Any(x => x.Id != roverId && x.Position == newPosition);
            if (!otherRoversExistsInNewLocation && newPosition != rover.Position)
                Emit(new RoverMovedEvent(roverId.Value, newPosition));

            return Task.FromResult(ExecutionResult.Success());
        }
    }
}