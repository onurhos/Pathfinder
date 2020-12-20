using EventFlow.Commands;
using Pathfinder.Domain.Aggregates.Plateau;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;

namespace Pathfinder.Commands.Plateau
{
    public class CreatePlateauCommand : Command<Domain.Aggregates.Plateau.Plateau, PlateauId>
    {
        public Boundary Boundary { get; }

        public CreatePlateauCommand
        (
            PlateauId aggregateId
            ,Boundary boundary
        ) : base(aggregateId)
        {
            Boundary = boundary;
        }
    }
}