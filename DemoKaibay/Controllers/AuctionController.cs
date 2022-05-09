using DemoKaibay.Dtos;
using DemoKaibay.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoKaibay.Controllers
{
    [Authorize]
    public class AuctionController : Controller
    {
        private readonly DemoKaibayService kaibayService;

        public AuctionController(DemoKaibayService kaibayService)
        {
            this.kaibayService=kaibayService;
        }

        [HttpGet("Auction/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAuctionItemDto value)
        {
            await kaibayService.CreateAuction(value, User);

            await kaibayService.GetActiveAuctions();

            return Ok();
        }

        [HttpGet("Auction/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var auctionDetails = await kaibayService.GetAuctionDetails(id);
            return View(auctionDetails);
        }
    }
}
