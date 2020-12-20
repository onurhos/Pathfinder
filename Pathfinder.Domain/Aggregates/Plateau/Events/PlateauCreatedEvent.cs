using EventFlow.Aggregates;
using EventFlow.EventStores;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;

namespace Pathfinder.Domain.Aggregates.Plateau.Events
{
    [EventVersion("PlateauCreated", 1)]
    public class PlateauCreatedEvent : AggregateEvent<Plateau, PlateauId>
    {
        public Boundary Boundary { get; }

        private PlateauCreatedEvent()
        {   
        }

        public PlateauCreatedEvent(Boundary boundary) : this()
        {
            Boundary = boundary;
        }
    }
}
