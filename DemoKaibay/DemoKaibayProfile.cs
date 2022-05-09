using AutoMapper;
using DemoKaibay.Dtos;
using DemoKaibay.Models;

namespace DemoKaibay
{
    public class DemoKaibayProfile : Profile
    {
        public DemoKaibayProfile()
        {
            CreateMap<CreateAuctionItemDto, Auction>();
            CreateMap<Auction, AuctionItemDto>();
            CreateMap<Auction, AuctionDetailsDto>();
            CreateMap<Bid, BidDto>().ReverseMap();
        }
    }
}
