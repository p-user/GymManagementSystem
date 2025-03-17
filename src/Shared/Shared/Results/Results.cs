using Shared.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Shared.Results
{
    public class Results
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public Results(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;

        }


        public static Results Success() => new(true, Error.None);
        public static Results Failure(Error error) => new(false, error);

        public static Results<TValue> Success<TValue>(TValue value) =>  new(value, true, Error.None);
        public static Results<TValue> Failure<TValue>(Error error) => new(default, false, error);


    }

    public class Results<TValue> : Results
    {
        private readonly TValue? _value;

        public Results(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        [NotNull]
        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can't be accessed.");

        public static implicit operator Results<TValue>(TValue? value) =>   value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

        public static Results<TValue> ValidationFailure(Error error) =>   new(default, false, error);
    }

}
