using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.Interfaces
{
    public interface ISportEventRepository
    {
        Task<IEnumerable<SportEvent>> GetAll();
        Task<SportEvent> GetByIdAsync(int id);
        Task<IEnumerable<SportEvent>> GetSportEventByCity(string city);
        bool Add(SportEvent sportEvent);
        bool Delete(SportEvent sportEvent);
        bool Update(SportEvent sportEvent);
        bool Save();
    }
}
