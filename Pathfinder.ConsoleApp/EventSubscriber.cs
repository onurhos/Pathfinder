using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Subscribers;
using Pathfinder.Domain.Aggregates.Plateau;
using Pathfinder.Domain.Aggregates.Plateau.Events;

namespace Pathfinder.ConsoleApp
{
    public class EventSubscriber :
        ISubscribeSynchronousTo<Plateau, PlateauId, PlateauCreatedEvent>,
        ISubscribeSynchronousTo<Plateau, PlateauId, RoverDeployedEvent>,
        ISubscribeSynchronousTo<Plateau, PlateauId, RoverDirectionChangedEvent>,
        ISubscribeSynchronousTo<Plateau, PlateauId, RoverMovedEvent>
    {
        public Task HandleAsync(IDomainEvent<Plateau, PlateauId, PlateauCreatedEvent> domainEvent, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{domainEvent.AggregateIdentity} created  {domainEvent.AggregateEvent.Boundary.Width} {domainEvent.AggregateEvent.Boundary.Height}");
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<Plateau, PlateauId, RoverDeployedEvent> domainEvent, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{domainEvent.AggregateEvent.Rover.Id} deployed at {domainEvent.AggregateEvent.Rover.Position} {domainEvent.AggregateEvent.Rover.Direction}");
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<Plateau, PlateauId, RoverDirectionChangedEvent> domainEvent, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{domainEvent.AggregateEvent.RoverId} rotated to {domainEvent.AggregateEvent.Direction}");
            return Task.CompletedTask;
        }

        public Task HandleAsync(IDomainEvent<Plateau, PlateauId, RoverMovedEvent> domainEvent, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{domainEvent.AggregateEvent.RoverId} moved to {domainEvent.AggregateEvent.Position}");
            return Task.CompletedTask;
        }
    }
}