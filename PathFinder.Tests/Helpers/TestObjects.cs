using System.Collections.Generic;
using Pathfinder.Domain.Aggregates.Plateau.Types;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;
using Pathfinder.ReadModel.Plateau;

namespace PathFinder.Tests.Helpers
{
    public static class TestObjects
    {
        public static Boundary Boundary = new Boundary(5, 5);

        public static RoverReadModel Rover1 =
            new RoverReadModel("1C1C763C-75A8-49D8-A33D-72A808F86D2E", Direction.N, new Position(1, 1));

        public static RoverReadModel Rover2 =
            new RoverReadModel("F8418B84-F35E-4686-A48D-736B9355DA32", Direction.E, new Position(2, 1));

        public static PlateauReadModel Plateau => new PlateauReadModel("144A7D79-03FF-4B6E-96F0-9324C3B583DE", Boundary, new List<RoverReadModel>());

    }
}
