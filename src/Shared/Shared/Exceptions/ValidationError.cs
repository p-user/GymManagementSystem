
namespace Shared.Exceptions
{
    public record ValidationError : Error
    {
        public ValidationError(Error[] errors)
       : base("General.Validation", "One or more validation errors occurred", ErrorType.Validation)
        {
            Errors = errors;
        }

        public Error[] Errors { get; }

        public static ValidationError FromResults(IEnumerable<Results.Results> results) =>
            new(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());
    }
}
