using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.Logs;
using Pathfinder.Domain;

namespace PathFinder.Tests.Helpers
{
    public class DomainTest : Test, IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly IAggregateStore AggregateStore;
        protected readonly ICommandBus CommandBus;

        public DomainTest()
        {  
            Resolver = EventFlowOptions.New
                .RegisterModule<DomainModule>()
                .CreateResolver();

            AggregateStore = Resolver.Resolve<IAggregateStore>();
            CommandBus = Resolver.Resolve<ICommandBus>();
        }
        
        protected async Task UpdateAsync<TAggregate, TIdentity>(TIdentity id, Action<TAggregate> action)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
        {
            await AggregateStore.UpdateAsync<TAggregate, TIdentity>(
                    id,
                    SourceId.New,
                    (a, c) =>
                    {
                        action(a);
                        return Task.FromResult(0);
                    },
                    CancellationToken.None)
                .ConfigureAwait(false);
        }

        public void Dispose()
        {
            Resolver?.DisposeSafe(new ConsoleLog(), "");
        }
    }
}