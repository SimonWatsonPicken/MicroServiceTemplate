using MicroService.Domain.Aggregates;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicroService.Application.ViewModels
{
    public class BookViewModel
    {
        [JsonPropertyName("title")]
        public string Title { get; }

        public List<string> Authors { get; }

        public List<string> Publishers { get; }

        public string PublishDate { get; }

        public string FirstSentence { get; }

        public string Isbn10 { get; }

        public string Isbn13 { get; }

        public BookViewModel(Book book)
        {
            Title = book.Title;
            Authors = book.Authors;
            Publishers = book.Publishers;
            PublishDate = book.PublishDate;
            FirstSentence = book.FirstSentence;
            Isbn10 = book.Isbn.Isbn10;
            Isbn13 = book.Isbn.Isbn13;
        }
    }
}