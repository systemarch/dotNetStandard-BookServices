namespace GoogleBooksService
{
    /// <summary>
    /// Represents a volume.
    /// See <see href="https://developers.google.com/books/docs/v1/reference/volumes#resource-representations"/> for reference.
    /// </summary>
    public class Volume
    {
        public string Id { get; set; }
        public VolumeInfo VolumeInfo { get; set; }
        public SaleInfo SaleInfo { get; set; }
    }
}
