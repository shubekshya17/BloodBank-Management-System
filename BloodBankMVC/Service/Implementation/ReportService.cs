using BloodBankMVC.Data;
using BloodBankMVC.Models;
using BloodBankMVC.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;
using BloodBankMVC.viewModel;
namespace BloodBankMVC.Service.Implementation
{
public class ReportService
{
    private readonly BloodBankContext  _context;

    public ReportService(BloodBankContext  context)
    {
        _context = context;
    }

    public async Task<BloodReportViewModel> GetDashboardAsync()
    {
        var model = new BloodReportViewModel();

        var conn = _context.Database.GetDbConnection();

        await conn.OpenAsync();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = "sp_GetBloodReportDashboard";
        cmd.CommandType = CommandType.StoredProcedure;

        using var reader = await cmd.ExecuteReaderAsync();

        // 1. Summary
        if (await reader.ReadAsync())
        {
            model.TotalDonors = reader.GetInt32(0);
            model.TotalRequests = reader.GetInt32(1);
            model.TotalUnitsDonated = reader.GetInt32(2);
            model.TotalUnitsRequested = reader.GetInt32(3);
        }

        // 2. Blood Groups
        await reader.NextResultAsync();
        while (await reader.ReadAsync())
        {
            model.BloodGroups.Add(new ChartData
            {
                Label = reader.GetString(0),
                Value = reader.GetInt32(1)
            });
        }

        // 3. Gender
        await reader.NextResultAsync();
        while (await reader.ReadAsync())
        {
            model.Gender.Add(new ChartData
            {
                Label = reader.GetString(0),
                Value = reader.GetInt32(1)
            });
        }

        // 4. Monthly
        await reader.NextResultAsync();
        while (await reader.ReadAsync())
        {
            model.Monthly.Add(new ChartData
            {
                Label = reader.GetString(0),
                Value = reader.GetInt32(1)
            });
        }

        // 5. Inventory
        await reader.NextResultAsync();
        while (await reader.ReadAsync())
        {
            model.Inventory.Add(new ChartData
            {
                Label = reader.GetString(0),
                Value = reader.GetInt32(1)
            });
        }

        await conn.CloseAsync();

        return model;
    }
    }
}