using System.Diagnostics.Contracts;
using Pathfinder.Domain.Exceptions.Common;

namespace Pathfinder.Domain.Exceptions
{
    public static class Fail
    {

        [Pure]
        public static FailureExecutionResultHasNoErrorMessageException BecauseFailureExecutionResultHasNoErrorMessage()
        {
            return new FailureExecutionResultHasNoErrorMessageException();
        }
    }
}
