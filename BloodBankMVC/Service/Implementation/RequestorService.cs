using BloodBankMVC.Data;
using BloodBankMVC.Models;
using BloodBankMVC.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloodBankMVC.Service.Implementation
{
    public class RequestorService : IRequestorService
    {
        private readonly BloodBankContext _context;
        private readonly IBloodInventoryService _bloodInventoryService;
        private readonly IAuditService _auditService;

        public RequestorService(BloodBankContext context, IBloodInventoryService bloodInventoryService, IAuditService auditService)
        {
            _context = context;
            _bloodInventoryService = bloodInventoryService;
            _auditService = auditService;
        }

        public async Task<List<Requestor>> GetAllRequestorsAsync()
        {
            return await _context.Requestors
                .Include(r => r.BloodGroup)
                .OrderByDescending(r => r.Date)
                .ToListAsync();
        }

        public async Task<List<Requestor>> GetPendingRequestorsAsync()
        {
            return await _context.Requestors
                .Include(r => r.BloodGroup)
                .Where(r => r.Status == 0)
                .OrderByDescending(r => r.Date)
                .ToListAsync();
        }

        public async Task<Requestor> GetRequestorByIdAsync(int id)
        {
            return await _context.Requestors
                .Include(r => r.BloodGroup)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> AddRequestorAsync(Requestor requestor)
        {
            try
            {
                requestor.Status = 0; // Set to Pending
                _context.Requestors.Add(requestor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AssignBloodAsync(int id)
        {
            try
            {
                var requestor = await GetRequestorByIdAsync(id);
                if (requestor == null || requestor.Status != 0)
                    return false;

                // Check if sufficient blood is available
                var available = await _bloodInventoryService.GetAvailableUnitsAsync(requestor.BloodGroupId);
                if (available < requestor.UnitRequested)
                    return false;

                // Update requestor status
                requestor.Status = 1;

                // Deduct from collection
                await _bloodInventoryService.DeductUnitsAsync(requestor.BloodGroupId, requestor.UnitRequested);

                // Create audit record
                await _auditService.CreateAuditAsync(new Audit
                {
                    Date = DateTime.Now,
                    RequestorId = requestor.Id,
                    Unit = requestor.UnitRequested,
                    ActionType = "Request"
                });

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RejectRequestAsync(int id)
        {
            try
            {
                var requestor = await GetRequestorByIdAsync(id);
                if (requestor == null || requestor.Status != 0)
                    return false;

                requestor.Status = 2;
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
