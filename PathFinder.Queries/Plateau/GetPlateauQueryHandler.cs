using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EventFlow.Queries;
using Pathfinder.ReadModel.Plateau;

namespace PathFinder.Queries.Plateau
{
    public class GetPlateauQueryHandler : IQueryHandler<GetPlateauQuery, Maybe<PlateauReadModel>>
    {
        private readonly IPlateauStore _plateauStore;

        public GetPlateauQueryHandler(IPlateauStore plateauStore) => _plateauStore = plateauStore;

        public async Task<Maybe<PlateauReadModel>> ExecuteQueryAsync(GetPlateauQuery query, CancellationToken cancellationToken) => 
            await _plateauStore.Get(query.Id, cancellationToken);
    }
}