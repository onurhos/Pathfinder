using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Pathfinder.Domain.Aggregates.Plateau.Entities
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class RoverId : Identity<RoverId>
    {
        public RoverId(string value) : base(value)
        {
        }
    }
}