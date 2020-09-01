using System.Collections.Generic;

namespace GoogleBooksService
{
    public class VolumeInfo
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public string Description { get; set; }
        public string MaturityRating { get; set; }
        public IEnumerable<IndustryIdentifier> IndustryIdentifiers { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public ImageLinks ImageLinks { get; set; }
        public string Language { get; set; }
        public string CanonicalVolumeLink { get; set; }
    }

    public class IndustryIdentifier
    {
        public string Type { get; set; }
        public string Identifier { get; set; }
    }

    public class ImageLinks
    {
        public string SmallThumbnail { get; set; }
        public string Thumbnail { get; set; }
    }
}
