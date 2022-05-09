using AutoMapper;
using DemoKaibay.Data;
using DemoKaibay.Dtos;
using DemoKaibay.Extensions;
using DemoKaibay.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DemoKaibay.Services
{
    public class DemoKaibayService
    {
        private const int MinimumIndex = 0;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> user;
        private readonly ApplicationDbContext dbContext;

        public DemoKaibayService(IMapper mapper, UserManager<IdentityUser> user, ApplicationDbContext dbContext)
        {
            this.mapper=mapper;
            this.dbContext=dbContext;
            this.user=user;
        }

        public async Task CreateAuction(CreateAuctionItemDto value, ClaimsPrincipal claimUser) 
        {
            var auction = mapper.Map<Auction>(value);
            auction.CreatorId = user.GetUserId(claimUser);
            auction.AuctionStarted = DateTime.Now;

            await dbContext.Auctions.AddAsync(auction);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IList<AuctionItemDto>> GetActiveAuctions()
        {
            var activeAuctions = await dbContext.Auctions.Include(x => x.Bids)
                                                         .Where(x => x.AuctionEnded > DateTime.Now)
                                                         .ToListAsync();

            foreach(var item in activeAuctions)
            {
                if (item.Bids.Count() > MinimumIndex)
                    item.CurrentPrice = item.Bids.Max(x => x.Price);
                else
                    item.CurrentPrice = item.InitialPrice;
            }

            return mapper.Map<List<AuctionItemDto>>(activeAuctions);
        }

        public async Task<AuctionDetailsDto> GetAuctionDetails(int id)
        {
            var auction = await dbContext.Auctions.Include(x => x.Bids).FirstOrDefaultAsync(x => x.Id == id);

            var auctionDetail = mapper.Map<AuctionDetailsDto>(auction);
            
            foreach (var item in auctionDetail.Bids)
            {
                item.BuyerName = user.Users.FirstOrDefault(x => x.Id == item.BuyerId)
                                           .UserName
                                           .AnonymizeEmail();
            }

            if(auction.Bids.Any())
            {
                auctionDetail.CurrentPrice = auction.Bids.Max(x => x.Price);
            }

            if(auctionDetail.AuctionEnded < DateTime.Now)
            {
                auctionDetail.IsEnded = true;
                auctionDetail.FinalPrice = auctionDetail.Bids.Any() ? auctionDetail.Bids.Max(x => x.Price) : Double.NaN;
            }

            return auctionDetail;
        }

        public async Task PostBid(BidDto bid)
        {
            var auction = await GetAuctionDetails(bid.AuctionId);

            switch(auction)
            {
                case null:
                    throw new Exception("Not Exist");
                case AuctionDetailsDto _auction when _auction.IsEnded == true:
                    throw new Exception("Auction Ended");
            }

            var bidIsAcceptable = BidIsAcceptable(bid.AuctionId, bid.Price);

            switch(bidIsAcceptable)
            {
                case true:
                    var bidMapper = mapper.Map<Bid>(bid);
                    dbContext.Bids.Add(bidMapper);
                    dbContext.SaveChanges();
                    break;

                case false:
                    throw new Exception("Bid not Acceptable");
            }

        }

        private bool BidIsAcceptable(int auctionId, double bidPrice)
        {
            var auction = dbContext.Auctions.Include(x => x.Bids).FirstOrDefault(x => x.Id == auctionId);
            var bidIsExist = auction.Bids.Any();
            
            if (bidIsExist != false)
            {
                var maxPrice = auction.Bids.Max(x => x.Price);
                return PriceComparaison(bidPrice, maxPrice);
                
            }
            else
            {
                return PriceComparaison(bidPrice, auction.InitialPrice, true);
            }


            bool PriceComparaison(double bidPrice, double mainMaxAuctionPrice, bool isFirstPrice=false)
            {
                if(isFirstPrice)
                {
                    return true ? bidPrice >= mainMaxAuctionPrice : false;
                }
                else
                {
                    return true ? bidPrice > mainMaxAuctionPrice : false;
                }
            }
        }
    }
}
