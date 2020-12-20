using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using Pathfinder.Domain.Aggregates.Plateau;

namespace Pathfinder.Commands.Plateau
{
    public class MoveRoverCommandHandler : CommandHandler<Domain.Aggregates.Plateau.Plateau, PlateauId, IExecutionResult, MoveRoverCommand>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync
        (
            Domain.Aggregates.Plateau.Plateau aggregate,
            MoveRoverCommand command,
            CancellationToken cancellationToken
        )
        {
            return await aggregate.MoveRover(command.RoverId);
        }
    }
}