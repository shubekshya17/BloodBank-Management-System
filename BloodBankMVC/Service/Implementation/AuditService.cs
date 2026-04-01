using BloodBankMVC.Data;
using BloodBankMVC.Models;
using BloodBankMVC.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloodBankMVC.Service.Implementation
{
    public class AuditService : IAuditService
    {
        private readonly BloodBankContext _context;

        public AuditService(BloodBankContext context)
        {
            _context = context;
        }
        public async Task<List<Audit>> GetAllAuditsAsync()
        {
            return await _context.Audits
                .Include(a => a.Donor)
                .Include(a => a.Requestor)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<bool> CreateAuditAsync(Audit audit)
        {
            try
            {
                _context.Audits.Add(audit);
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