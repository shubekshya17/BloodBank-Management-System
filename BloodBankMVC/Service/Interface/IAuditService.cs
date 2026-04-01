using BloodBankMVC.Models;

namespace BloodBankMVC.Service.Interface
{
    public interface IAuditService
    {
        Task<List<Audit>> GetAllAuditsAsync();
        Task<bool> CreateAuditAsync(Audit audit);
    }
}
