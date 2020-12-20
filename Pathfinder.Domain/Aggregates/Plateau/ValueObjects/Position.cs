using EventFlow.ValueObjects;
using Pathfinder.Domain.Aggregates.Plateau.Types;

namespace Pathfinder.Domain.Aggregates.Plateau.ValueObjects
{
    public class Position : ValueObject
    {
        public int X { get; }
        public int Y { get; }

        private Position() {}
        public Position(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"{X} {Y}";

        public Position MoveTo(Direction direction, Boundary boundary)
        {
            switch (direction)
            {
                case Direction.N:
                {
                    var nextPosition = Y + 1;
                    return nextPosition > boundary.Height ? this : new Position(X, nextPosition);
                }
                case Direction.E:
                {
                    var nextPosition = X + 1;
                    return nextPosition > boundary.Width ? this : new Position(nextPosition, Y);
                }
                case Direction.S:
                {
                    var nextPosition = Y - 1;
                    return nextPosition < 0 ? this : new Position(X, nextPosition);
                }
                case Direction.W:
                {
                    var nextPosition = X - 1;
                    return nextPosition < 0 ? this : new Position(nextPosition, Y);
                }
                default:
                    return this;
            }
        }
    }
}