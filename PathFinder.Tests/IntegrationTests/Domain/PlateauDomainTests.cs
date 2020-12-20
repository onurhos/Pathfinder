using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Pathfinder.Domain.Aggregates.Plateau;
using PathFinder.Tests.Helpers;
using Xunit;

namespace PathFinder.Tests.IntegrationTests.Domain
{
    [Collection(Collections.Integration)]
    public class PlateauDomainTests : DomainTest
    {
        [Fact]
        public async Task Create_New_Plateau_Should_Load_The_Aggregate()
        {
            var id = PlateauId.New;

            static void CreateNewPlateau(Plateau p) => p.CreatePlateau(TestObjects.Boundary);
            await UpdateAsync(id, (Action<Plateau>)CreateNewPlateau);

            var aggregate = await AggregateStore.LoadAsync<Plateau, PlateauId>(id, CancellationToken.None);
            aggregate.Id.Should().Be(id);
        }
    }
}