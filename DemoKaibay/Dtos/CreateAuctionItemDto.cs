using DemoKaibay.Models;
using System.ComponentModel.DataAnnotations;

namespace DemoKaibay.Dtos
{
    public class CreateAuctionItemDto
    {
        [MaxLength(KaibayConsts.MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(KaibayConsts.MaxDescriptionLength)]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public double InitialPrice { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yy hh:mm} ")]
        public DateTime AuctionEnded { get; set; }

    }
}
