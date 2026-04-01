using BloodBankMVC.Models;

namespace BloodBankMVC.Service.Interface
{
    public interface IRequestorService
    {
        Task<List<Requestor>> GetAllRequestorsAsync();
        Task<List<Requestor>> GetPendingRequestorsAsync();
        Task<Requestor> GetRequestorByIdAsync(int id);
        Task<bool> AddRequestorAsync(Requestor requestor);
        Task<bool> AssignBloodAsync(int id);
        Task<bool> RejectRequestAsync(int id);
    }
}
