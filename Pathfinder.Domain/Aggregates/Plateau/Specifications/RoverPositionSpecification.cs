using System.Collections.Generic;
using EventFlow.Specifications;
using Pathfinder.Domain.Aggregates.Plateau.Entities;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;

namespace Pathfinder.Domain.Aggregates.Plateau.Specifications
{
    public class RoverPositionSpecification : Specification<Rover>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(Rover rover)
        {
            if (rover == null)
                yield return "Invalid rover position";

            var maxBoundary = Boundary.Max;
            if (rover != null && (
                rover.Position.X < 0
                || rover.Position.Y < 0
                || rover.Position.X > maxBoundary.Width
                || rover.Position.Y > maxBoundary.Height
                ))
                yield return "Rover position out of plateau bounds";
        }
    }
}