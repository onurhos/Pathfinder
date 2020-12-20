using System.Linq;
using CSharpFunctionalExtensions;
using EventFlow.Aggregates.ExecutionResults;
using Pathfinder.Domain.Exceptions;

namespace Pathfinder.Application.Services
{
    public class ErrorProvider
    {
        public static Result<T, ServiceError> MapExecutionResultError<T>(IExecutionResult executionResult, T value)
        {
            if (executionResult is FailedExecutionResult failedExecution)
            {
                if (failedExecution.Errors == null || !failedExecution.Errors.Any())
                    throw Fail.BecauseFailureExecutionResultHasNoErrorMessage();
                return Result.Failure<T, ServiceError>(new ServiceError(failedExecution.Errors.ToArray()));
            }

            return Result.Success<T, ServiceError>(value);
        }

        public static Maybe<ServiceError> MapExecutionResultErrorMaybe(IExecutionResult executionResult)
        {
            if (executionResult is FailedExecutionResult failedExecution)
            {
                if (failedExecution.Errors == null || !failedExecution.Errors.Any())
                    throw Fail.BecauseFailureExecutionResultHasNoErrorMessage();
                return new ServiceError(failedExecution.Errors.ToArray());
            }
            return Maybe<ServiceError>.None;
        }
    }
}