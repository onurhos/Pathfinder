using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace PathFinder.Queries
{
    public class QueriesModule : IModule
    {
        public static Assembly Assembly { get; } = typeof(QueriesModule).Assembly;

        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .AddDefaults(Assembly);
        }
    }
}