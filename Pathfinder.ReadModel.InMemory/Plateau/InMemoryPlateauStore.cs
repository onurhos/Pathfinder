using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EventFlow.ReadStores.InMemory;
using Pathfinder.ReadModel.Plateau;

namespace Pathfinder.ReadModel.InMemory.Plateau
{
    public class InMemoryPlateauStore : IPlateauStore
    {
        private readonly IInMemoryReadStore<PlateauInMemoryReadModel> _readStore;

        public InMemoryPlateauStore(IInMemoryReadStore<PlateauInMemoryReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<Maybe<PlateauReadModel>> Get(string id, CancellationToken cancellationToken)
        {
            var records = await _readStore.FindAsync(rm => rm.Id == id, cancellationToken);
            if (records == null || !records.Any())
                return Maybe<PlateauReadModel>.None;

            var readModel = records
            .Select(x => new PlateauReadModel(x.Id, x.Boundary, x.Rovers.Select(x => x.ToReadModel()).ToList()))
            .FirstOrDefault();

            return readModel ?? Maybe<PlateauReadModel>.None;
        }
    }
}