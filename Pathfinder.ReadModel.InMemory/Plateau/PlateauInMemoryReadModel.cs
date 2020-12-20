using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Pathfinder.Domain.Aggregates.Plateau;
using Pathfinder.Domain.Aggregates.Plateau.Events;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;

namespace Pathfinder.ReadModel.InMemory.Plateau
{
    public class PlateauInMemoryReadModel : IReadModel,
        IAmReadModelFor<Domain.Aggregates.Plateau.Plateau, PlateauId, PlateauCreatedEvent>,
        IAmReadModelFor<Domain.Aggregates.Plateau.Plateau, PlateauId, RoverDeployedEvent>,
        IAmReadModelFor<Domain.Aggregates.Plateau.Plateau, PlateauId, RoverDirectionChangedEvent>,
        IAmReadModelFor<Domain.Aggregates.Plateau.Plateau, PlateauId, RoverMovedEvent>
    {
        public PlateauInMemoryReadModel()
        {
            Rovers = new List<RoverInMemoryReadModel>();
        }

        public string Id { get; set; }
        public Boundary Boundary { get; set; }
        public List<RoverInMemoryReadModel> Rovers { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<Domain.Aggregates.Plateau.Plateau, PlateauId, PlateauCreatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Boundary = domainEvent.AggregateEvent.Boundary;
        }

        public void Apply(IReadModelContext context, IDomainEvent<Domain.Aggregates.Plateau.Plateau, PlateauId, RoverDeployedEvent> domainEvent)
        {
            Rovers.Add(RoverInMemoryReadModel.FromDomainModel(domainEvent.AggregateEvent.Rover));
        }

        public void Apply(IReadModelContext context, IDomainEvent<Domain.Aggregates.Plateau.Plateau, PlateauId, RoverDirectionChangedEvent> domainEvent)
        {
            var rover = Rovers.FirstOrDefault(x => x.Id == domainEvent.AggregateEvent.RoverId);
            if (rover == null) return;

            rover.Direction = domainEvent.AggregateEvent.Direction;
        }

        public void Apply(IReadModelContext context, IDomainEvent<Domain.Aggregates.Plateau.Plateau, PlateauId, RoverMovedEvent> domainEvent)
        {
            var rover = Rovers.FirstOrDefault(x => x.Id == domainEvent.AggregateEvent.RoverId);
            if (rover == null) return;

            rover.Position = domainEvent.AggregateEvent.Position;
        }
    }
}