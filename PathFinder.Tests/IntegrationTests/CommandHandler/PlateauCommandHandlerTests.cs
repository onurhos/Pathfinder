using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Pathfinder.Commands.Plateau;
using Pathfinder.Domain.Aggregates.Plateau;
using PathFinder.Tests.Helpers;
using Xunit;

namespace PathFinder.Tests.IntegrationTests.CommandHandler
{
    [Collection(Collections.Integration)]
    public class PlateauCommandHandlerTests : CommandHandlerTest
    {
        [Fact]
        public async Task Create_Plateau_Command_Should_Load_The_Aggregate()
        {
            var id = PlateauId.New;

            var command = new CreatePlateauCommand(id, TestObjects.Boundary);
            _ = await CommandBus.PublishAsync(command, CancellationToken.None);

            var aggregate = await AggregateStore.LoadAsync<Plateau, PlateauId>(id, CancellationToken.None);
            aggregate.Id.Should().Be(id);
            aggregate.Boundary.Should().Be(TestObjects.Boundary);
        }
    }
}