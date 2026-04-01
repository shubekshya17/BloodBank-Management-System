using BloodBankMVC.Models;

namespace BloodBankMVC.Service.Interface
{
    public interface IBloodInventoryService
    {
        Task<List<BloodInventory>> GetAllCollectionsAsync();
        Task<int> GetAvailableUnitsAsync(int bloodGroupId);
        Task<bool> AddUnitsAsync(int bloodGroupId, int units);
        Task<bool> DeductUnitsAsync(int bloodGroupId, int units);
    }
}
