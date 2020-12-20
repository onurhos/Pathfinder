using System;
using Pathfinder.Domain.Aggregates.Plateau.Types;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;

namespace Pathfinder.ReadModel.Plateau
{
    public class RoverReadModel
    {
        public string Id { get; set; }
        public Direction Direction { get; set; }
        public Position Position { get; set; }

        private RoverReadModel() { }
        public RoverReadModel(string id, Direction direction, Position position) : this()
        {
            Id = id;
            Direction = direction;
            Position = position;
        }

        public void PrintToConsole()
        {
            Console.WriteLine($"Position: {Position}, Direction: {Direction}");
        }
    }
}