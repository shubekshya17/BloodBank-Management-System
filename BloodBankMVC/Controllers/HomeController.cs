using System.Diagnostics;
using BloodBankMVC.Models;
using BloodBankMVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBloodInventoryService _bloodInventoryService;

        public HomeController(IBloodInventoryService bloodInventoryService)
        {
            _bloodInventoryService = bloodInventoryService;
        }

        public async Task<IActionResult> Index()
        {
            var collections = await _bloodInventoryService.GetAllCollectionsAsync();
            return View(collections);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
