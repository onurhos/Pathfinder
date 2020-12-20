using System;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Extensions;
using EventFlow.Logs;
using Pathfinder.Commands;
using Pathfinder.Domain;

namespace PathFinder.Tests.Helpers
{
    public class CommandHandlerTest : Test, IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly IAggregateStore AggregateStore;
        protected readonly ICommandBus CommandBus;

        public CommandHandlerTest()
        {
            Resolver = EventFlowOptions.New
                .RegisterModule<DomainModule>()
                .RegisterModule<CommandsModule>()
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