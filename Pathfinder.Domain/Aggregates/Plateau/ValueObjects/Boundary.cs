using EventFlow.ValueObjects;

namespace Pathfinder.Domain.Aggregates.Plateau.ValueObjects
{
    public class Boundary : ValueObject
    {
        public static int MaxValue = 50;
        public static int MinValue = 2;
        public static Boundary Min => new Boundary(MinValue, MinValue);
        public static Boundary Max => new Boundary(MaxValue, MaxValue);
        
        public int Width { get; }
        public int Height { get; }

        private Boundary() { }
        public Boundary(int width, int height) : this()
        {
            Width = width;
            Height = height;
        }

        public override string ToString() => $"{Width} x {Height}";
    }
}