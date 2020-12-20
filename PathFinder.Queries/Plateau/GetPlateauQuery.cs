using CSharpFunctionalExtensions;
using EventFlow.Queries;
using Pathfinder.ReadModel.Plateau;

namespace PathFinder.Queries.Plateau
{
    public class GetPlateauQuery : IQuery<Maybe<PlateauReadModel>>
    {
        public string Id { get; }

        private GetPlateauQuery() { }
        public GetPlateauQuery(string id) : this()
        {
            Id = id;
        }
    }
}