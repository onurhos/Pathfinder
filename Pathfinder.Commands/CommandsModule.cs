using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace Pathfinder.Commands
{
    public class CommandsModule : IModule
    {
        public static Assembly Assembly { get; } = typeof(CommandsModule).Assembly;

        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .AddDefaults(Assembly);
        }
    }
}