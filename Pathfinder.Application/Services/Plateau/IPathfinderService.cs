using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Pathfinder.Domain.Aggregates.Plateau;
using Pathfinder.Domain.Aggregates.Plateau.Entities;
using Pathfinder.Domain.Aggregates.Plateau.Types;
using Pathfinder.Domain.Aggregates.Plateau.ValueObjects;
using Pathfinder.ReadModel.Plateau;

namespace Pathfinder.Application.Services.Plateau
{
    public interface IPathfinderService
    {
        Task<Result<PlateauId, ServiceError>> CreatePlateauAsync(Boundary boundary);
        Task<Maybe<ServiceError>> DeployRoverAsync(PlateauId plateauId, RoverId roverId, Position position, Direction direction);
        Task<Maybe<ServiceError>> RotateRoverAsync(PlateauId plateauId, RoverId roverId, Rotate rotate);
        Task<Maybe<ServiceError>> MoveRoverAsync(PlateauId plateauId, RoverId roverId);
        Task<Maybe<PlateauReadModel>> GetPlateauStateAsync(PlateauId plateauId);
    }
}