using MicroService.Domain.Aggregates;
using System.Collections.Generic;

namespace MicroService.Api.Application.Books
{
    public class BookUiEntity
    {
        public string Title { get; set; }

        public List<string> Authors { get; set; }

        public List<string> Publishers { get; set; }

        public string PublishDate { get; set; }

        public string FirstSentence { get; set; }

        public string Isbn { get; set; }

        public List<string> Errors { get; set; }

        public BookUiEntity(BookInformation bookInformation)
        {
            Title = bookInformation.Title;
            Authors = bookInformation.Authors;
            Publishers = bookInformation.Publishers;
            PublishDate = bookInformation.PublishDate;
            FirstSentence = bookInformation.FirstSentence;
            Isbn = bookInformation.Isbn;
            Errors = bookInformation.Errors;
        }
    }
}