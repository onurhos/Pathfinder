using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace Pathfinder.Domain
{
    public class DomainModule : IModule
    {
        public static Assembly Assembly { get; } = typeof(DomainModule).Assembly;

        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .AddDefaults(Assembly);
        }
    }
}