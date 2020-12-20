using Pathfinder.Domain.Aggregates.Plateau.Entities;
using Pathfinder.Domain.Aggregates.Plateau.Types;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;
using Pathfinder.ReadModel.Plateau;

namespace Pathfinder.ReadModel.InMemory.Plateau
{
    public class RoverInMemoryReadModel
    {
        public string Id { get; set; }
        public Direction Direction { get; set; }
        public Position Position { get; set; }

        public static RoverInMemoryReadModel FromDomainModel(Rover rover) => new RoverInMemoryReadModel
        {
            Position = rover.Position,
            Direction = rover.Direction,
            Id = rover.Id.Value
        };

        public RoverReadModel ToReadModel() => new RoverReadModel(Id, Direction, Position);
    }
}