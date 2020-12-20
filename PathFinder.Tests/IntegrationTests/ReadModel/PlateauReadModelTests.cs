using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.ReadStores.InMemory;
using FluentAssertions;
using Pathfinder.Commands.Plateau;
using Pathfinder.Domain.Aggregates.Plateau;
using Pathfinder.ReadModel.InMemory.Plateau;
using PathFinder.Tests.Helpers;
using Xunit;

namespace PathFinder.Tests.IntegrationTests.ReadModel
{
    [Collection(Collections.Integration)]
    public class PlateauReadModelTests : ReadModelTest
    {
        private readonly IInMemoryReadStore<PlateauInMemoryReadModel> _plateauReadModel;

        public PlateauReadModelTests()
        {
            _plateauReadModel = Resolver.Resolve<IInMemoryReadStore<PlateauInMemoryReadModel>>();
        }

        [Fact]
        public async Task InMemory_Plateau_ReadModel_Should_Contains_Data()
        {
            var id = PlateauId.New;

            var createNewPlateauCommand = new CreatePlateauCommand(id, TestObjects.Boundary);

            await CommandBus.PublishAsync(createNewPlateauCommand, CancellationToken.None);

            var readModels = await _plateauReadModel.FindAsync(x => true, CancellationToken.None);
            readModels.Should().NotBeNull();
            readModels.Count.Should().Be(1);

            var readModel = readModels.FirstOrDefault();
            readModel.Should().NotBeNull();
            if (readModel != null)
            {
                readModel.Id.Should().Be(id.Value);
                readModel.Boundary.Should().Be(TestObjects.Boundary);
            }
        }
    }
}