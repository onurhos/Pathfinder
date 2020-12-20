using EventFlow.Entities;
using Pathfinder.Domain.Aggregates.Plateau.Types;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;

namespace Pathfinder.Domain.Aggregates.Plateau.Entities
{
    public class Rover : Entity<RoverId>
    {
        public Position Position { get; set; }
        public Direction Direction { get; set; }

        public Rover(RoverId id, Position position, Direction direction) : base(id)
        {
            Position = position;
            Direction = direction;
        }
    }
}
