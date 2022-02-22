using System.Collections.Generic;

namespace MicroService.Domain.Aggregates
{
    public class Book
    {
        public string Title { get; set; }

        public List<string> Authors { get; set; }

        public List<string> Publishers { get; set; }

        public string PublishDate { get; set; }

        public string FirstSentence { get; set; }

        public Isbn Isbn { get; set; }

        public Book()
        {
            Authors = new List<string>();
        }
    }
}