using EventFlow.Commands;
using Pathfinder.Domain.Aggregates.Plateau;
using Pathfinder.Domain.Aggregates.Plateau.Entities;

namespace Pathfinder.Commands.Plateau
{
    public class MoveRoverCommand : Command<Domain.Aggregates.Plateau.Plateau, PlateauId>
    {
        public RoverId RoverId { get; }

        public MoveRoverCommand
        (
            PlateauId aggregateId
            ,RoverId roverId
        ) : base(aggregateId)
        {
            RoverId = roverId;
        }
    }
}