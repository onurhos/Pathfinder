using System;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Extensions;
using EventFlow.Logs;
using Pathfinder.Commands;
using Pathfinder.Domain;
using PathFinder.Queries;
using Pathfinder.ReadModel.InMemory;

namespace PathFinder.Tests.Helpers
{
    public class QueryHandlerTest : Test, IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly IAggregateStore AggregateStore;
        protected readonly ICommandBus CommandBus;

        public QueryHandlerTest()
        {
            Resolver = EventFlowOptions.New
                .RegisterModule<DomainModule>()
                .RegisterModule<CommandsModule>()
                .RegisterModule<InMemoryReadModelModule>()
                .RegisterModule<QueriesModule>()
                .CreateResolver();

            AggregateStore = Resolver.Resolve<IAggregateStore>();
            CommandBus = Resolver.Resolve<ICommandBus>();
        }

        public void Dispose()
        {
            Resolver?.DisposeSafe(new ConsoleLog(), "");
        }
    }
}