using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<SportEvent>> GetAllEvents();
        Task<List<SportGroup>> GetAllGroups();
        Task<AppUser> GetUserById(string id);
        Task<AppUser>GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
