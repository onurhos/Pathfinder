using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Pathfinder.ReadModel.Plateau
{
    public interface IPlateauStore
    {
        Task<Maybe<PlateauReadModel>> Get(string id, CancellationToken cancellationToken);
    }
}
