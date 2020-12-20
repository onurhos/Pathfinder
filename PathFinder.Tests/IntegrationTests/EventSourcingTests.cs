using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;
using FluentAssertions;
using Pathfinder.Commands.Plateau;
using Pathfinder.Domain.Aggregates.Plateau;
using Pathfinder.Domain.Aggregates.Plateau.Entities;
using Pathfinder.Domain.Aggregates.Plateau.Types;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;
using PathFinder.Queries.Plateau;
using Pathfinder.ReadModel.Plateau;
using PathFinder.Tests.Helpers;
using Xunit;

namespace PathFinder.Tests.IntegrationTests
{
    [Collection(Collections.Integration)]
    public class EventSourcingTests : QueryHandlerTest
    {
        private readonly IQueryHandler<GetPlateauQuery, Maybe<PlateauReadModel>> _queryHandler;

        public EventSourcingTests()
        {
            _queryHandler = Resolver.Resolve<IQueryHandler<GetPlateauQuery, Maybe<PlateauReadModel>>>();
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        public async Task Plateau_Boundaries_Should_Be_Valid(int width, int height)
        {
            //  Negative
            var boundary = new Boundary(width, height);
            var createCommand = new CreatePlateauCommand(PlateauId.New, boundary);
            var executionResult = await CommandBus.PublishAsync(createCommand, CancellationToken.None);
            executionResult.IsSuccess.Should().BeFalse();
            executionResult.Should().BeOfType<FailedExecutionResult>();
        }

        [Fact]
        public async Task Plateau_Boundary_Limit_Tests()
        {
            var boundary = new Boundary(Boundary.MinValue - 1, 5);
            var createCommand = new CreatePlateauCommand(PlateauId.New, boundary);
            var executionResult = await CommandBus.PublishAsync(createCommand, CancellationToken.None);
            executionResult.IsSuccess.Should().BeFalse();
            executionResult.Should().BeOfType<FailedExecutionResult>();

            boundary = new Boundary(5, Boundary.MinValue - 1);
            createCommand = new CreatePlateauCommand(PlateauId.New, boundary);
            executionResult = await CommandBus.PublishAsync(createCommand, CancellationToken.None);
            executionResult.IsSuccess.Should().BeFalse();
            executionResult.Should().BeOfType<FailedExecutionResult>();

            boundary = new Boundary(Boundary.MinValue - 1, Boundary.MinValue - 1);
            createCommand = new CreatePlateauCommand(PlateauId.New, boundary);
            executionResult = await CommandBus.PublishAsync(createCommand, CancellationToken.None);
            executionResult.IsSuccess.Should().BeFalse();
            executionResult.Should().BeOfType<FailedExecutionResult>();

            boundary = new Boundary(Boundary.MaxValue + 1, 5);
            createCommand = new CreatePlateauCommand(PlateauId.New, boundary);
            executionResult = await CommandBus.PublishAsync(createCommand, CancellationToken.None);
            executionResult.IsSuccess.Should().BeFalse();
            executionResult.Should().BeOfType<FailedExecutionResult>();

            boundary = new Boundary(5, Boundary.MaxValue + 1);
            createCommand = new CreatePlateauCommand(PlateauId.New, boundary);
            executionResult = await CommandBus.PublishAsync(createCommand, CancellationToken.None);
            executionResult.IsSuccess.Should().BeFalse();
            executionResult.Should().BeOfType<FailedExecutionResult>();

            boundary = new Boundary(Boundary.MaxValue + 1, Boundary.MaxValue + 1);
            createCommand = new CreatePlateauCommand(PlateauId.New, boundary);
            executionResult = await CommandBus.PublishAsync(createCommand, CancellationToken.None);
            executionResult.IsSuccess.Should().BeFalse();
            executionResult.Should().BeOfType<FailedExecutionResult>();
        }

        [Fact]
        public async Task Deploy_Rover_Should_Be_Ok()
        {
            var plateauId = PlateauId.New;

            //  Create plateau
            var boundary = new Boundary(2, 2);
            var createPlateauCommand = new CreatePlateauCommand(plateauId, boundary);
            var createPlateauCommandExecutionResult = await CommandBus.PublishAsync(createPlateauCommand, CancellationToken.None);
            createPlateauCommandExecutionResult.IsSuccess.Should().BeTrue();

            //  Deploy rover
            var roverId = RoverId.New;
            var roverPosition = new Position(1, 1);
            const Direction roverDirection = Direction.N;
            var rover = new Rover(roverId, roverPosition, roverDirection);
            var deployRoverCommand = new DeployRoverCommand(plateauId, rover);
            var deployRoverCommandExecutionResult = await CommandBus.PublishAsync(deployRoverCommand, CancellationToken.None);
            deployRoverCommandExecutionResult.IsSuccess.Should().BeTrue();

            //  Query plateau state
            var query = new GetPlateauQuery(plateauId.Value);
            var maybeReadModel = await _queryHandler.ExecuteQueryAsync(query, CancellationToken.None);
            maybeReadModel.HasValue.Should().BeTrue();

            var readModel = maybeReadModel.Value;
            readModel.Should().NotBeNull();
            if (readModel != null)
            {
                readModel.Id.Should().Be(plateauId.Value);
                readModel.Boundary.Should().Be(boundary);
                readModel.Rovers.Count.Should().Be(1);
                var rover1 = readModel.Rovers.FirstOrDefault();
                rover1.Should().NotBeNull();
                if (rover1 != null)
                {
                    rover1.Id.Should().Be(roverId.Value);
                    rover1.Position.Should().Be(roverPosition);
                    rover1.Direction.Should().Be(roverDirection);
                }
            }
        }

        [Fact]
        public async Task Rotate_Rover_Should_Be_Ok()
        {
            var plateauId = PlateauId.New;

            //  Create plateau
            var boundary = new Boundary(2, 2);
            var createPlateauCommand = new CreatePlateauCommand(plateauId, boundary);
            var createPlateauCommandExecutionResult = await CommandBus.PublishAsync(createPlateauCommand, CancellationToken.None);
            createPlateauCommandExecutionResult.IsSuccess.Should().BeTrue();

            //  Deploy rover
            var roverId = RoverId.New;
            var roverPosition = new Position(1, 1);
            const Direction roverDirection = Direction.N;
            var rover = new Rover(roverId, roverPosition, roverDirection);
            var deployRoverCommand = new DeployRoverCommand(plateauId, rover);
            var deployRoverCommandExecutionResult = await CommandBus.PublishAsync(deployRoverCommand, CancellationToken.None);
            deployRoverCommandExecutionResult.IsSuccess.Should().BeTrue();

            //  Rotate rover
            const Rotate rotate = Rotate.R;
            const Direction newRoverDirection = Direction.E;
            var rotateRoverCommand = new RotateRoverCommand(plateauId, roverId, rotate);
            var rotateRoverCommandExecutionResult = await CommandBus.PublishAsync(rotateRoverCommand, CancellationToken.None);
            rotateRoverCommandExecutionResult.IsSuccess.Should().BeTrue();

            //  Query plateau state
            var query = new GetPlateauQuery(plateauId.Value);
            var maybeReadModel = await _queryHandler.ExecuteQueryAsync(query, CancellationToken.None);
            maybeReadModel.HasValue.Should().BeTrue();

            var readModel = maybeReadModel.Value;
            readModel.Should().NotBeNull();
            if (readModel != null)
            {
                readModel.Id.Should().Be(plateauId.Value);
                readModel.Boundary.Should().Be(boundary);
                readModel.Rovers.Count.Should().Be(1);
                var rover1 = readModel.Rovers.FirstOrDefault();
                rover1.Should().NotBeNull();
                if (rover1 != null)
                {
                    rover1.Id.Should().Be(roverId.Value);
                    rover1.Position.Should().Be(roverPosition);
                    rover1.Direction.Should().Be(newRoverDirection);
                }
            }
        }

        [Fact]
        public async Task Move_Rover_Should_Be_Ok()
        {
            var plateauId = PlateauId.New;

            //  Create plateau
            var boundary = new Boundary(2, 2);
            var createPlateauCommand = new CreatePlateauCommand(plateauId, boundary);
            var createPlateauCommandExecutionResult = await CommandBus.PublishAsync(createPlateauCommand, CancellationToken.None);
            createPlateauCommandExecutionResult.IsSuccess.Should().BeTrue();

            //  Deploy rover
            var roverId = RoverId.New;
            var roverPosition = new Position(1, 1);
            const Direction roverDirection = Direction.N;
            var rover = new Rover(roverId, roverPosition, roverDirection);
            var deployRoverCommand = new DeployRoverCommand(plateauId, rover);
            var deployRoverCommandExecutionResult = await CommandBus.PublishAsync(deployRoverCommand, CancellationToken.None);
            deployRoverCommandExecutionResult.IsSuccess.Should().BeTrue();

            //  Move rover
            var moveRoverCommand = new MoveRoverCommand(plateauId, roverId);
            var moveRoverCommandExecutionResult = await CommandBus.PublishAsync(moveRoverCommand, CancellationToken.None);
            moveRoverCommandExecutionResult.IsSuccess.Should().BeTrue();

            //  Query plateau state
            var query = new GetPlateauQuery(plateauId.Value);
            var maybeReadModel = await _queryHandler.ExecuteQueryAsync(query, CancellationToken.None);
            maybeReadModel.HasValue.Should().BeTrue();

            var readModel = maybeReadModel.Value;
            readModel.Should().NotBeNull();
            if (readModel != null)
            {
                readModel.Id.Should().Be(plateauId.Value);
                readModel.Boundary.Should().Be(boundary);
                readModel.Rovers.Count.Should().Be(1);
                var rover1 = readModel.Rovers.FirstOrDefault();
                rover1.Should().NotBeNull();
                if (rover1 != null)
                {
                    rover1.Id.Should().Be(roverId.Value);
                    rover1.Position.Should().Be(new Position(roverPosition.X, roverPosition.Y + 1));
                    rover1.Direction.Should().Be(roverDirection);
                }
            }
        }
    }
}