using System.Collections.Generic;

namespace MicroService.Domain.Aggregates
{
    public class BookInformation
    {
        public string Title { get; set; }

        public List<string> Authors { get; set; }

        public List<string> Publishers { get; set; }

        public string PublishDate { get; set; }

        public string FirstSentence { get; set; }

        public string Isbn { get; set; }

        public List<string> Errors { get; set; }

        public bool IsValid => Errors.Count == 0;

        public BookInformation()
        {
            Authors = new List<string>();
            Errors = new List<string>();
        }
    }
}