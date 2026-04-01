using BloodBankMVC.Models;

namespace BloodBankMVC.Service.Interface
{
    public interface IBloodGroupService
    {
        Task<List<BloodGroup>> GetAllBloodGroupsAsync();
        Task<BloodGroup> GetBloodGroupByIdAsync(int id);
    }
}
