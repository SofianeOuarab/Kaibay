using DemoKaibay.Dtos;
using DemoKaibay.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace DemoKaibay
{
   
    public class DemoKaibayHub : Hub
    {
        private readonly DemoKaibayService kaibayService;
        private readonly UserManager<IdentityUser> user;

        public DemoKaibayHub(DemoKaibayService kaibayService, UserManager<IdentityUser> user)
        {
            this.kaibayService=kaibayService;
            this.user=user;
        }

        public async Task UpdateBidCurrentPrice (int auctionId, double newPrice)
        {
            await Clients.All.SendAsync("updateBidCurrentPrice", auctionId, newPrice);
        }

        public async Task NotifyBidUpdate(int auctionId, double newPrice)
        {
            await Clients.Group(auctionId.ToString()).SendAsync("NotifyBidUpdate", auctionId, newPrice);
        }


        [Authorize]
        [HubMethodName("BidRequest")]
        public async Task BidRequest(int auctionId, double priceBid)
        {
            var bid = new BidDto();
            bid.Price = priceBid;
            bid.AuctionId = auctionId;
            bid.BuyerId = user.GetUserId(Context.User);
            bid.BuyerName = user.GetUserName(Context.User);

            try
            {
                await kaibayService.PostBid(bid);
                await Clients.All.SendAsync("UpdatePrice", auctionId, priceBid);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Reject", auctionId, ex.Message);
            }
        }
    }
}
