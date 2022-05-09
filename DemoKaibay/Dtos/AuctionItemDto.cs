namespace DemoKaibay.Dtos
{
    public class AuctionItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double InitialPrice { get; set; }
        public double? CurrentPrice { get; set; }
        public DateTime AuctionEnded { get; set; }
    }
}
