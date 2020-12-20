using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using Pathfinder.Domain.Aggregates.Plateau.Entities;
using Pathfinder.Domain.Aggregates.Plateau.Events;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;

namespace Pathfinder.Domain.Aggregates.Plateau
{
    public sealed class PlateauState : AggregateState<Plateau, PlateauId, PlateauState>, 
        IApply<PlateauCreatedEvent>,
        IApply<RoverDeployedEvent>,
        IApply<RoverDirectionChangedEvent>,
        IApply<RoverMovedEvent>
    {
        public Boundary Boundary { get; set; }
        public IReadOnlyList<Rover> Rovers => _rovers.AsReadOnly();
        private List<Rover> _rovers;

        public void Apply(PlateauCreatedEvent aggregateEvent)
        {
            Boundary = aggregateEvent.Boundary;
            _rovers = new List<Rover>();
        }

        public void Apply(RoverDeployedEvent aggregateEvent)
        {
            _rovers.Add(aggregateEvent.Rover);
        }

        public void Apply(RoverDirectionChangedEvent aggregateEvent)
        {
            var rover = _rovers.FirstOrDefault(x => x.Id.Value == aggregateEvent.RoverId);
            if (rover == null) return;

            rover.Direction = aggregateEvent.Direction;
        }

        public void Apply(RoverMovedEvent aggregateEvent)
        {
            var rover = _rovers.FirstOrDefault(x => x.Id.Value == aggregateEvent.RoverId);
            if (rover == null) return;

            rover.Position = aggregateEvent.Position;
        }
    }
}