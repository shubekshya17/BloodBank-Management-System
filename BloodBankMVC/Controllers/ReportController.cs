using BloodBankMVC.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using BloodBankMVC.Service.Implementation;

namespace BloodBankMVC.Controllers
{
    public class ReportController : Controller
    {
         private readonly ReportService _service;

    public ReportController(ReportService service)
    {
        _service = service;
    }
        // GET: Report
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var data = await _service.GetDashboardAsync();
            return View(data);
        }
    }
}