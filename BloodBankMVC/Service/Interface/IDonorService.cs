using BloodBankMVC.Models;

namespace BloodBankMVC.Service.Interface
{
    public interface IDonorService
    {
        Task<List<Donor>> GetAllDonorsAsync();
        Task<List<Donor>> GetPendingDonorsAsync();
        Task<Donor> GetDonorByIdAsync(int id);
        Task<bool> AddDonorAsync(Donor donor);
        Task<bool> ApproveDonorAsync(int id);
        Task<bool> RejectDonorAsync(int id);
    }
}
