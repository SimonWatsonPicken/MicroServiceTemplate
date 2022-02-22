using System;
using System.Collections.Generic;

namespace MicroService.Domain.SeedWork
{
    [Serializable]
    public struct Result<T>
    {
        public const string ErrorObjectIsNotProvidedForFailure =
            "You attempted to create a failure result, which must have an error, but a null error object (or empty string) was passed to the constructor.";

        public const string ErrorObjectIsProvidedForSuccess =
            "You attempted to create a success result, which cannot have an error, but a non-null error object was passed to the constructor.";

        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;

        public T Value { get; }

        public Error Error { get; }

        internal Result(bool isFailure, Error error, T value)
        {
            IsFailure = ErrorStateGuard(isFailure, error);
            Error = error;
            Value = value;
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>(true, error, default);
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(false, default, value);
        }

        public override string ToString()
        {
            return IsSuccess
                ? $"Success({Value})"
                : $"Failure({Error})";
        }

        private static bool ErrorStateGuard(bool isFailure, Error error)
        {
            switch (isFailure)
            {
                case true when error == null:
                    throw new ArgumentNullException(nameof(error), ErrorObjectIsNotProvidedForFailure);
                case false when !EqualityComparer<Error>.Default.Equals(error, default):
                    throw new ArgumentException(ErrorObjectIsProvidedForSuccess, nameof(error));
                default:
                    return isFailure;
            }
        }
    }
}