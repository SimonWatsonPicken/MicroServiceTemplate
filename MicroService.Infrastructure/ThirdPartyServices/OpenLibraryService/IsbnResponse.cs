using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicroService.Infrastructure.ThirdPartyServices.OpenLibraryService
{
    public class IsbnResponse
    {
        [JsonPropertyName("publishers")]
        public List<string> Publishers { get; set; }

        [JsonPropertyName("number_of_pages")]
        public int NumberOfPages { get; set; }

        [JsonPropertyName("isbn_10")]
        public List<string> Isbn10 { get; set; }

        [JsonPropertyName("isbn_13")]
        public List<string> Isbn13 { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("authors")]
        public List<Author> Authors { get; set; }

        [JsonPropertyName("contributions")]
        public List<string> Contributions { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("publish_date")]
        public string PublishDate { get; set; }

        [JsonPropertyName("works")]
        public List<Work> Works { get; set; }

        [JsonPropertyName("first_sentence")]
        public FirstSentence FirstSentence { get; set; }
    }

    public class Author
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }

    public class Work
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }

    public class FirstSentence
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}