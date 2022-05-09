using DemoKaibay.Models;
using DemoKaibay.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoKaibay.Controllers
{
    public class HomeController : Controller
    {
        private readonly DemoKaibayService itemService;

        public HomeController(DemoKaibayService itemService)
        {
            this.itemService=itemService;
        }

        public async Task<IActionResult> Index()
        {
            var activeAuctions = await itemService.GetActiveAuctions();
            return View(activeAuctions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}