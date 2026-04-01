using BloodBankMVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDonorService _donorService;
        private readonly IRequestorService _requestorService;
        private readonly IBloodInventoryService _bloodInventoryService;
        private readonly IAuditService _auditService;

        // Hardcoded credentials
        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin123";

        public AdminController(
            IDonorService donorService,
            IRequestorService requestorService,
            IBloodInventoryService bloodInventoryService,
            IAuditService auditService)
        {
            _donorService = donorService;
            _requestorService = requestorService;
            _bloodInventoryService = bloodInventoryService;
            _auditService = auditService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // If already logged in, redirect to dashboard
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (username == AdminUsername && password == AdminPassword)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                TempData["Success"] = "Login successful!";
                return RedirectToAction("Dashboard");
            }
            else
            {
                TempData["Error"] = "Invalid username or password!";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Logged out successfully!";
            return RedirectToAction("Index", "Home");
        }

        private bool IsAuthenticated()
        {
            return HttpContext.Session.GetString("IsAdmin") == "true";
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            ViewBag.Collections = await _bloodInventoryService.GetAllCollectionsAsync();
            ViewBag.PendingDonors = (await _donorService.GetPendingDonorsAsync()).Count;
            ViewBag.PendingRequests = (await _requestorService.GetPendingRequestorsAsync()).Count;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PendingDonors()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            var donors = await _donorService.GetPendingDonorsAsync();
            return View(donors);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveDonor(int id)
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            var result = await _donorService.ApproveDonorAsync(id);
            if (result)
                TempData["Success"] = "Donor approved successfully!";
            else
                TempData["Error"] = "Failed to approve donor.";

            return RedirectToAction("PendingDonors");
        }

        [HttpPost]
        public async Task<IActionResult> RejectDonor(int id)
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            var result = await _donorService.RejectDonorAsync(id);
            if (result)
                TempData["Success"] = "Donor rejected successfully!";
            else
                TempData["Error"] = "Failed to reject donor.";

            return RedirectToAction("PendingDonors");
        }

        [HttpGet]
        public async Task<IActionResult> PendingRequests()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            var requestors = await _requestorService.GetPendingRequestorsAsync();
            ViewBag.Collections = await _bloodInventoryService.GetAllCollectionsAsync();
            return View(requestors);
        }

        [HttpPost]
        public async Task<IActionResult> AssignBlood(int id)
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            var result = await _requestorService.AssignBloodAsync(id);
            if (result)
                TempData["Success"] = "Blood assigned successfully!";
            else
                TempData["Error"] = "Failed to assign blood";

            return RedirectToAction("PendingRequests");
        }

        [HttpPost]
        public async Task<IActionResult> RejectRequest(int id)
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            var result = await _requestorService.RejectRequestAsync(id);
            if (result)
                TempData["Success"] = "Request rejected successfully!";
            else
                TempData["Error"] = "Failed to reject request.";

            return RedirectToAction("PendingRequests");
        }

        [HttpGet]
        public async Task<IActionResult> AllDonors()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            var donors = await _donorService.GetAllDonorsAsync();
            return View(donors);
        }

        [HttpGet]
        public async Task<IActionResult> AllRequests()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            var requestors = await _requestorService.GetAllRequestorsAsync();
            return View(requestors);
        }

        [HttpGet]
        public async Task<IActionResult> Audits()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            var audits = await _auditService.GetAllAuditsAsync();
            return View(audits);
        }
    }
}
