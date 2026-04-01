using BloodBankMVC.Data;
using BloodBankMVC.Models;
using BloodBankMVC.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloodBankMVC.Service.Implementation
{
    public class BloodGroupService : IBloodGroupService
    {
        private readonly BloodBankContext _context;

        public BloodGroupService(BloodBankContext context)
        {
            _context = context;
        }
        public async Task<List<BloodGroup>> GetAllBloodGroupsAsync()
        {
            return await _context.BloodGroups.ToListAsync();
        }

        public async Task<BloodGroup> GetBloodGroupByIdAsync(int id)
        {
            return await _context.BloodGroups.FindAsync(id);
        }
    }
}

