using EventFlow.Commands;
using Pathfinder.Domain.Aggregates.Plateau;
using Pathfinder.Domain.Aggregates.Plateau.Entities;

namespace Pathfinder.Commands.Plateau
{
    public class DeployRoverCommand : Command<Domain.Aggregates.Plateau.Plateau, PlateauId>
    {
        public Rover Rover { get; }

        public DeployRoverCommand
        (
            PlateauId aggregateId
            ,Rover rover
        ) : base(aggregateId)
        {
            Rover = rover;
        }
    }
}