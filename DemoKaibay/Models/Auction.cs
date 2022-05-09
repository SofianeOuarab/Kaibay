using System.ComponentModel.DataAnnotations;

namespace DemoKaibay.Models
{
    public class Auction
    {
        public int Id { get; }

        [MaxLength(KaibayConsts.MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(KaibayConsts.MaxDescriptionLength)]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public string CreatorId { get; set; }

        public DateTime AuctionStarted { get; set; }

        public DateTime AuctionEnded { get; set; }

        [Required]
        public double InitialPrice { get; set; }

        public double CurrentPrice { get; set; }

        public double FinalPrice { get; set; }

        public ICollection<Bid> Bids { get; set; }

    }
}
