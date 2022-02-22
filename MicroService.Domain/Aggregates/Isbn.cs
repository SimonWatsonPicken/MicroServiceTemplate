using MicroService.Domain.Exceptions;
using MicroService.Domain.SeedWork;
using System.Collections.Generic;

namespace MicroService.Domain.Aggregates
{
    public sealed class Isbn : ValueObject
    {
        private readonly string _rawIsbn;

        public string Isbn10 { get; private set; }

        public string Isbn13 { get; private set; }

        public Isbn(string rawIsbn)
        {
            _rawIsbn = rawIsbn;
            Validate();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Isbn10;
            yield return Isbn13;
        }

        protected override void Validate()
        {
            var isbn = _rawIsbn.Replace("-", "").Replace(" ", "");
            string validIsbn;

            switch (isbn.Length)
            {
                case 10:
                    validIsbn = isbn.Substring(0, 9) + Isbn10Checksum(isbn);
                    if (isbn != validIsbn)
                    {
                        throw new IsbnValidationException($"{isbn} is not a valid ISBN-10 number.");
                    }

                    Isbn10 = isbn;
                    Isbn13 = ConvertIsbn10ToIsbn13(Isbn10);
                    break;
                case 13:
                    validIsbn = isbn.Substring(0, 12) + Isbn13Checksum(isbn);
                    if (isbn != validIsbn)
                    {
                        throw new IsbnValidationException($"{isbn} is not a valid ISBN-13 number.");
                    }

                    Isbn13 = isbn;
                    Isbn10 = ConvertIsbn13ToIsbn10(Isbn13);
                    break;
                default:
                    throw new IsbnValidationException($"{isbn} is neither a valid ISBN-10 or ISBN-13 number.");
            }

        }
        private static string Isbn10Checksum(string isbn)
        {
            var sum = 0;
            for (var isbnCharacterIndex = 0; isbnCharacterIndex < 9; isbnCharacterIndex++)
            {
                sum += (10 - isbnCharacterIndex) * int.Parse(isbn[isbnCharacterIndex].ToString());
            }

            var remainder = sum % 11;
            return remainder switch
            {
                0 => "0",
                1 => "X",
                _ => (11 - remainder).ToString()
            };
        }

        private static string Isbn13Checksum(string isbn)
        {
            var sum = 0;
            for (var isbnCharacterIndex = 0; isbnCharacterIndex < 12; isbnCharacterIndex++)
            {
                sum += (isbnCharacterIndex % 2 == 0 ? 1 : 3) * int.Parse(isbn[isbnCharacterIndex].ToString());
            }

            var remainder = sum % 10;
            return remainder == 0
                ? "0"
                : (10 - remainder).ToString();
        }

        private static string ConvertIsbn10ToIsbn13(string isbn)
        {
            var isbn13 = "978" + isbn.Substring(0, 9);
            return isbn13 + Isbn13Checksum(isbn13);
        }

        private static string ConvertIsbn13ToIsbn10(string isbn)
        {
            var isbn10 = isbn.Substring(3, 9);
            return isbn10 + Isbn10Checksum(isbn10);
        }
    }
}