using BloodBankMVC.Data;
using BloodBankMVC.Models;
using BloodBankMVC.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloodBankMVC.Service.Implementation
{
    public class BloodInventoryService : IBloodInventoryService
    {
        private readonly BloodBankContext _context;

        public BloodInventoryService(BloodBankContext context)
        {
            _context = context;
        }

        public async Task<List<BloodInventory>> GetAllCollectionsAsync()
        {
            return await _context.BloodInventories
                .Include(c => c.BloodGroup)
                .OrderBy(c => c.BloodGroupId)
                .ToListAsync();
        }

        public async Task<int> GetAvailableUnitsAsync(int bloodGroupId)
        {
            var collection = await _context.BloodInventories
                .FirstOrDefaultAsync(c => c.BloodGroupId == bloodGroupId);
            return collection?.Quantity ?? 0;
        }

        public async Task<bool> AddUnitsAsync(int bloodGroupId, int units)
        {
            var collection = await _context.BloodInventories
                .FirstOrDefaultAsync(c => c.BloodGroupId == bloodGroupId);

            if (collection == null)
            {
                // Create new collection entry if not exists
                collection = new BloodInventory
                {
                    BloodGroupId = bloodGroupId,
                    Quantity = units
                };
                _context.BloodInventories.Add(collection);
            }
            else
            {
                collection.Quantity += units;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeductUnitsAsync(int bloodGroupId, int units)
        {
            var collection = await _context.BloodInventories
                .FirstOrDefaultAsync(c => c.BloodGroupId == bloodGroupId);

            if (collection == null || collection.Quantity < units)
                return false;

            collection.Quantity -= units;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
