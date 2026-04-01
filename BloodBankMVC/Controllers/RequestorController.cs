using BloodBankMVC.Models;
using BloodBankMVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankMVC.Controllers
{
    public class RequestorController : Controller
    {
        private readonly IRequestorService _requestorService;
        private readonly IBloodGroupService _bloodGroupService;

        public RequestorController(IRequestorService requestorService, IBloodGroupService bloodGroupService)
        {
            _requestorService = requestorService;
            _bloodGroupService = bloodGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> Request()
        {
            ViewBag.BloodGroups = await _bloodGroupService.GetAllBloodGroupsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Request(Requestor requestor)
        {
            if (ModelState.IsValid)
            {
                var result = await _requestorService.AddRequestorAsync(requestor);
                if (result)
                {
                    TempData["Success"] = "Your blood request has been submitted successfully! Please wait for admin approval.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Failed to submit blood request. Please try again.";
                }
            }

            ViewBag.BloodGroups = await _bloodGroupService.GetAllBloodGroupsAsync();
            return View(requestor);
        }
    }
}
