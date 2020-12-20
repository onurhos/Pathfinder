using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using Pathfinder.Domain.Aggregates.Plateau;

namespace Pathfinder.Commands.Plateau
{
    public class RotateRoverCommandHandler : CommandHandler<Domain.Aggregates.Plateau.Plateau, PlateauId, IExecutionResult, RotateRoverCommand>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync
        (
            Domain.Aggregates.Plateau.Plateau aggregate,
            RotateRoverCommand command,
            CancellationToken cancellationToken
        )
        {
            return await aggregate.RotateRover(command.RoverId, command.Rotate);
        }
    }
}