using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Pathfinder.Domain.Aggregates.Plateau
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public sealed class PlateauId : Identity<PlateauId>
    {
        public PlateauId(string value) : base(value)
        {
        }
    }
}