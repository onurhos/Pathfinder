using System.Collections.Generic;
using EventFlow.Specifications;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;

namespace Pathfinder.Domain.Aggregates.Plateau.Specifications
{
    public class PlateauBoundaryValidationSpecification : Specification<Boundary>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(Boundary boundary)
        {
            if (boundary == null)
                yield return "Invalid plateau boundary";

            var minBoundary = Boundary.Min;
            if (boundary != null && (boundary.Width < minBoundary.Width || boundary.Height < minBoundary.Height))
                yield return "Minimum plateau boundaries should be 2 x 2";

            var maxBoundary = Boundary.Max;
            if (boundary != null && (boundary.Width > maxBoundary.Width || boundary.Height > maxBoundary.Height))
                yield return "Maximum plateau boundaries should be 50 x 50";
        }
    }
}