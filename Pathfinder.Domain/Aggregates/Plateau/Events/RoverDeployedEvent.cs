using EventFlow.Aggregates;
using EventFlow.EventStores;
using Pathfinder.Domain.Aggregates.Plateau.Entities;

namespace Pathfinder.Domain.Aggregates.Plateau.Events
{
    [EventVersion("RoverDeployed", 1)]
    public class RoverDeployedEvent : AggregateEvent<Plateau, PlateauId>
    {
        public Rover Rover { get; }

        private RoverDeployedEvent()
        {   
        }

        public RoverDeployedEvent(Rover rover) : this()
        {
            Rover = rover;
        }
    }
}
