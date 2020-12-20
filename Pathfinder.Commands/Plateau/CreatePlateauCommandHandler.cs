using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using Pathfinder.Domain.Aggregates.Plateau;

namespace Pathfinder.Commands.Plateau
{
    public class CreatePlateauCommandHandler : CommandHandler<Domain.Aggregates.Plateau.Plateau, PlateauId, IExecutionResult, CreatePlateauCommand>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync
        (
            Domain.Aggregates.Plateau.Plateau aggregate,
            CreatePlateauCommand command,
            CancellationToken cancellationToken
        )
        {
            return await aggregate.CreatePlateau(command.Boundary);
        }
    }
}