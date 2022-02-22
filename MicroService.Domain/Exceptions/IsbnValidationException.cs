using System;

namespace MicroService.Domain.Exceptions
{
    public class IsbnValidationException : Exception
    {
        public IsbnValidationException(string message) : base(message)
        {
        }
    }
}