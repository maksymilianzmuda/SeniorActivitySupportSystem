using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.Interfaces
{
    public interface ISportGroupRepository
    {
        Task<IEnumerable<SportGroup>> GetAll();
        Task<SportGroup> GetByIdAsync(int id);
        Task<IEnumerable<SportGroup>> GetSportGroupByCity(string city);
        bool Add(SportGroup sportGroup);    
        bool Delete(SportGroup sportGroup);
        bool Update(SportGroup sportGroup);
        bool Save();
    }
}
