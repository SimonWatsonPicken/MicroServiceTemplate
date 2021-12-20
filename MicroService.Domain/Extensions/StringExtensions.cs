using System;

namespace MicroService.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string ForceTrailingCharacter(this string valueToCheck, char trailingCharacter)
        {
            if (trailingCharacter == '\0') return valueToCheck;
            if (string.IsNullOrEmpty(valueToCheck)) return trailingCharacter.ToString();

            if (!valueToCheck.EndsWith(trailingCharacter))
            {
                valueToCheck += trailingCharacter;
            }

            return valueToCheck;
        }

        public static string StripLeadingCharacter(this string valueToCheck, char leadingCharacter)
        {
            if (string.IsNullOrEmpty(valueToCheck) || leadingCharacter == '\0') return valueToCheck;

            return valueToCheck.StartsWith(leadingCharacter)
                ? valueToCheck.TrimStart(leadingCharacter)
                : valueToCheck;
        }

        public static bool IsValidUri(this string uri)
        {
            return Uri.TryCreate(uri, UriKind.Absolute, out var uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}