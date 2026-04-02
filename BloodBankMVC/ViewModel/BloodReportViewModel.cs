using BloodBankMVC.Data;
using BloodBankMVC.Models;
using BloodBankMVC.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BloodBankMVC.viewModel
{
public class BloodReportViewModel
    {
        public int TotalDonors { get; set; }
        public int TotalRequests { get; set; }
        public int TotalUnitsDonated { get; set; }
        public int TotalUnitsRequested { get; set; }

        public List<ChartData> BloodGroups { get; set; } = new();
        public List<ChartData> Gender { get; set; } = new();
        public List<ChartData> Monthly { get; set; } = new();
        public List<ChartData> Inventory { get; set; } = new();
    }

        public class ChartData
        {
            public string? Label { get; set; }
            public int Value { get; set; }
        }
}
