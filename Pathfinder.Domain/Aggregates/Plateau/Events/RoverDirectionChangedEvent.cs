using EventFlow.Aggregates;
using EventFlow.EventStores;
using Pathfinder.Domain.Aggregates.Plateau.Types;

namespace Pathfinder.Domain.Aggregates.Plateau.Events
{
    [EventVersion("RoverDirectionChanged", 1)]
    public class RoverDirectionChangedEvent : AggregateEvent<Plateau, PlateauId>
    {
        public string RoverId { get; }
        public Direction Direction { get; }

        private RoverDirectionChangedEvent()
        {   
        }

        public RoverDirectionChangedEvent(string roverId, Direction direction) : this()
        {
            RoverId = roverId;
            Direction = direction;
        }
    }
}
