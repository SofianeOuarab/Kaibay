namespace DemoKaibay.Dtos
{
    public class BidDto
    {
        public int AuctionId { get; set; }
        public string BuyerId { get; set; }

        public double Price { get; set; }

        public DateTime BidTime { get; set; }

        public string BuyerName { get; set; }
    }
}
