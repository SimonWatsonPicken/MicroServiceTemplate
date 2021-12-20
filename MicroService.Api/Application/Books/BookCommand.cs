using System.Text.Json.Serialization;

namespace MicroService.Api.Application.Books
{
    public sealed class BookCommand
    {
        /// <summary>Gets or sets the ISBN.</summary>
        /// <value>The ISBN.</value>
        /// <example>0451526538</example>
        [JsonPropertyName("isbn")]
        public string Isbn { get; set; }
    }
}