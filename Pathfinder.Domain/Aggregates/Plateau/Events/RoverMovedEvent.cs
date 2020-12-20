using EventFlow.Aggregates;
using EventFlow.EventStores;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;

namespace Pathfinder.Domain.Aggregates.Plateau.Events
{
    [EventVersion("RoverMoved", 1)]
    public class RoverMovedEvent : AggregateEvent<Plateau, PlateauId>
    {
        public string RoverId { get; }
        public Position Position { get; }

        private RoverMovedEvent()
        {   
        }

        public RoverMovedEvent(string roverId, Position position) : this()
        {
            RoverId = roverId;
            Position = position;
        }
    }
}
