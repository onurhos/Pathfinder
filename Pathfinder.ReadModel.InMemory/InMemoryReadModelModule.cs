using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;
using Pathfinder.ReadModel.InMemory.Plateau;
using Pathfinder.ReadModel.Plateau;

namespace Pathfinder.ReadModel.InMemory
{
    public class InMemoryReadModelModule : IModule
    {
        public static Assembly Assembly { get; } = typeof(InMemoryReadModelModule).Assembly;

        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .AddDefaults(Assembly)
                .UseInMemoryReadStoreFor<PlateauInMemoryReadModel>()
                .RegisterServices(serviceRegistration =>
                 {
                     serviceRegistration.Register<IPlateauStore, InMemoryPlateauStore>();
                 });
        }
    }
}