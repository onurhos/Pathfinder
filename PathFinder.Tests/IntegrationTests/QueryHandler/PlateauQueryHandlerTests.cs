using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EventFlow.Queries;
using FluentAssertions;
using Pathfinder.Commands.Plateau;
using Pathfinder.Domain.Aggregates.Plateau;
using PathFinder.Queries.Plateau;
using Pathfinder.ReadModel.Plateau;
using PathFinder.Tests.Helpers;
using Xunit;

namespace PathFinder.Tests.IntegrationTests.QueryHandler
{
    [Collection(Collections.Integration)]
    public class Tests : QueryHandlerTest
    {
        private readonly IQueryHandler<GetPlateauQuery, Maybe<PlateauReadModel>> _queryHandler;

        public Tests()
        {
            _queryHandler = Resolver.Resolve<IQueryHandler<GetPlateauQuery, Maybe<PlateauReadModel>>>();
        }

        [Fact]
        public async Task Get_Plateau_Query_Should_Return_Value()
        {
            var id = PlateauId.New;
            var createNew = new CreatePlateauCommand(id, TestObjects.Boundary);
            await CommandBus.PublishAsync(createNew, CancellationToken.None);

            var query = new GetPlateauQuery(id.Value);
            var maybeReadModel = await _queryHandler.ExecuteQueryAsync(query, CancellationToken.None);
            maybeReadModel.HasValue.Should().BeTrue();

            var readModel = maybeReadModel.Value;
            readModel.Should().NotBeNull();
            if (readModel != null)
            {
                readModel.Id.Should().Be(id.Value);
                readModel.Boundary.Should().Be(TestObjects.Boundary);
            }
        }
    }
}