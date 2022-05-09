namespace DemoKaibay.Models
{
    public class Bid
    {
        public int Id { get; set; }

        public int AuctionId { get; set; }

        public Auction Auction { get; set; }

        public string BuyerId { get; set; }

        public double Price { get; set; }

        public DateTime BidTime { get; set; }
    }
}
