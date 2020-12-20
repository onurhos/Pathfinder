using EventFlow.Commands;
using Pathfinder.Domain.Aggregates.Plateau;
using Pathfinder.Domain.Aggregates.Plateau.Entities;
using Pathfinder.Domain.Aggregates.Plateau.Types;

namespace Pathfinder.Commands.Plateau
{
    public class RotateRoverCommand : Command<Domain.Aggregates.Plateau.Plateau, PlateauId>
    {
        public RoverId RoverId { get; }
        public Rotate Rotate { get; }

        public RotateRoverCommand
        (
            PlateauId aggregateId
            ,RoverId roverId
            ,Rotate rotate
        ) : base(aggregateId)
        {
            RoverId = roverId;
            Rotate = rotate;
        }
    }
}