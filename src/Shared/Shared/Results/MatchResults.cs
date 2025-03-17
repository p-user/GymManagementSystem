

namespace Shared.Results
{
    public static class MatchResults
    {
        // Match is a functional programming concept that allows you to match on the result of a computation.
        
        public static TOut Match<TOut>(this Results result, Func<TOut> onSuccess, Func<Results, TOut> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure(result);
        }

        public static TOut Match<TIn, TOut>(this Results<TIn> result, Func<TIn, TOut> onSuccess, Func<Results<TIn>, TOut> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
        }
    }
}
