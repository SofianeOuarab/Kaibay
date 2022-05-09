namespace DemoKaibay.Dtos
{
    public class AuctionDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double InitialPrice { get; set; }
        public double? CurrentPrice { get; set; }
        public double FinalPrice { get; set; }
        public DateTime AuctionEnded { get; set; }
        public bool IsEnded { get; set; }
        public ICollection<BidDto> Bids { get; set; }

    }
}
