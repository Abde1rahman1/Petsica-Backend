﻿using Petsica.Shared.Contracts.Community.Response;
using Petsica.Shared.Error;

namespace Petsica.Shared.Result
{
    public class Result
    {
        public Result(bool isSuccess, Errors error)
        {
            if ((isSuccess && error != Errors.None) || (!isSuccess && error == Errors.None))
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Errors Error { get; } = default!;

        public static Result Success() => new(true, Errors.None);
        public static Result Failure(Errors error) => new(false, error);

        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Errors.None);
        public static Result<TValue> Failure<TValue>(Errors error) => new(default, false, error);

		public static Result<PostResponse> Failure()
		{
			throw new NotImplementedException();
		}
	}

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        public Result(TValue? value, bool isSuccess, Errors error) : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Failure results cannot have value");
    }
}
