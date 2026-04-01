using BloodBankMVC.Models;
using BloodBankMVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankMVC.Controllers
{
    public class DonorController : Controller
    {
        private readonly IDonorService _donorService;
        private readonly IBloodGroupService _bloodGroupService;

        public DonorController(IDonorService donorService, IBloodGroupService bloodGroupService)
        {
            _donorService = donorService;
            _bloodGroupService = bloodGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> Donate()
        {
            ViewBag.BloodGroups = await _bloodGroupService.GetAllBloodGroupsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Donate(Donor donor)
        {
            if (ModelState.IsValid)
            {
                var result = await _donorService.AddDonorAsync(donor);
                if (result)
                {
                    TempData["Success"] = "Thank you for your donation request! Your submission is pending approval.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Failed to submit donation request. Please try again.";
                }
            }

            ViewBag.BloodGroups = await _bloodGroupService.GetAllBloodGroupsAsync();
            return View(donor);
        }

        [HttpGet]
        public async Task<IActionResult> Success()
        {
            return View();
        }
    }
}
