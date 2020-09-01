namespace GoogleBooksService
{
    public class SaleInfo
    {
        public string Saleability { get; set; }
        public Price RetailPrice { get; set; }
    }

    public class Price
    {
        public string Country { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}
