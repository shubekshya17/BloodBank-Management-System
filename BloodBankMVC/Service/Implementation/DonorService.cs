using BloodBankMVC.Data;
using BloodBankMVC.Models;
using BloodBankMVC.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloodBankMVC.Service.Implementation
{
    public class DonorService : IDonorService
    {
        private readonly BloodBankContext _context;
        private readonly IBloodInventoryService _bloodInventoryService;
        private readonly IAuditService _auditService;

        public DonorService(BloodBankContext context, IBloodInventoryService bloodInventoryService, IAuditService auditService)
        {
            _context = context;
            _bloodInventoryService = bloodInventoryService;
            _auditService = auditService;
        }

        public async Task<List<Donor>> GetAllDonorsAsync()
        {
            return await _context.Donors
                .Include(d => d.BloodGroup)
                .OrderByDescending(d => d.DonateDate)
                .ToListAsync();
        }

        public async Task<List<Donor>> GetPendingDonorsAsync()
        {
            return await _context.Donors
                .Include(d => d.BloodGroup)
                .Where(d => d.Status == 0)
                .OrderByDescending(d => d.DonateDate)
                .ToListAsync();
        }

        public async Task<Donor> GetDonorByIdAsync(int id)
        {
            return await _context.Donors
                .Include(d => d.BloodGroup)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> AddDonorAsync(Donor donor)
        {
            try
            {
                donor.Status = 0; // Set to Pending
                _context.Donors.Add(donor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ApproveDonorAsync(int id)
        {
            try
            {
                var donor = await GetDonorByIdAsync(id);
                if (donor == null || donor.Status != 0)
                    return false;

                // Update donor status
                donor.Status = 1;

                // Update collection
                await _bloodInventoryService.AddUnitsAsync(donor.BloodGroupId, donor.Unit);

                // Create audit record
                await _auditService.CreateAuditAsync(new Audit
                {
                    Date = DateTime.Now,
                    DonorId = donor.Id,
                    Unit = donor.Unit,
                    ActionType = "Donation"
                });

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RejectDonorAsync(int id)
        {
            try
            {
                var donor = await GetDonorByIdAsync(id);
                if (donor == null || donor.Status != 0)
                    return false;

                donor.Status = 2;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
