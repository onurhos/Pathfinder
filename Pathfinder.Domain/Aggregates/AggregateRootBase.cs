using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Core;

namespace Pathfinder.Domain.Aggregates
{
    public abstract class AggregateRootBase<TAggregate, TIdentity> : AggregateRoot<TAggregate, TIdentity>
        where TIdentity : IIdentity
        where TAggregate : AggregateRoot<TAggregate, TIdentity>
    {
        protected AggregateRootBase(TIdentity id) : base(id)
        {
        }

        protected Task<IExecutionResult> ExecuteIfSpecificationsValid(Action action, params Func<IExecutionResult>[] checks)
        {
            if (checks != null)
            {
                var errors = new List<string>();
                foreach (var check in checks)
                {
                    var executionResult = check();
                    if (!executionResult.IsSuccess && executionResult is FailedExecutionResult failedExecutionResult)
                        errors.AddRange(failedExecutionResult.Errors);
                }

                if (errors.Any())
                    return Task.FromResult(ExecutionResult.Failed(errors));
            }

            action();
            return Task.FromResult(ExecutionResult.Success());
        }
    }
}
