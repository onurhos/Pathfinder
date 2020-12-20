using System.Collections.Generic;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;

namespace Pathfinder.ReadModel.Plateau
{
    public class PlateauReadModel
    {
        public string Id { get; set; }
        public Boundary Boundary { get; set; }
        public List<RoverReadModel> Rovers { get; set; }

        private PlateauReadModel() { }
        public PlateauReadModel(string id, Boundary boundary, List<RoverReadModel> rovers) : this()
        {
            Id = id;
            Boundary = boundary;
            Rovers = rovers;
        }
    }
}