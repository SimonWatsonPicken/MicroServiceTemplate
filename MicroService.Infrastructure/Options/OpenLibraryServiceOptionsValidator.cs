using MicroService.Domain.Extensions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace MicroService.Infrastructure.Options
{
    public sealed class OpenLibraryServiceOptionsValidator : IValidateOptions<OpenLibraryServiceOptions>
    {
        public ValidateOptionsResult Validate(string name, OpenLibraryServiceOptions options)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(options.BaseAddress))
                errors.Add("The Open Library service base address cannot be null or empty.");

            if (!options.BaseAddress.IsValidUri())
                errors.Add($"The Open Library service base address '{options.BaseAddress}' is not a valid URI.");

            if (string.IsNullOrWhiteSpace(options.IsbnEndpoint))
                errors.Add("The Open Library service 'ISBN' endpoint cannot be null or empty.");

            return errors.Any()
                ? ValidateOptionsResult.Fail(errors)
                : ValidateOptionsResult.Success;
        }
    }
}