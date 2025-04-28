using Microsoft.EntityFrameworkCore;
using SeniorActivitySupportSystem.Data;
using SeniorActivitySupportSystem.Interfaces;
using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.Repository
{
    public class SportEventRepository : ISportEventRepository
    {
        private readonly ApplicationDbContext _context;
        public SportEventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(SportEvent sportEvent)
        {
            _context.Add(sportEvent);
            return Save();
        }

        public bool Delete(SportEvent sportEvent)
        {
            _context.Remove(sportEvent);
            return Save();
        }

        public async Task<IEnumerable<SportEvent>> GetAll()
        {
            return await _context.SportEvents.ToListAsync();
        }

        public async Task<SportEvent> GetByIdAsync(int id)
        {
            return await _context.SportEvents.Include(i =>i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<SportEvent>> GetSportEventByCity(string city)
        {
            return await _context.SportEvents.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(SportEvent sportEvent)
        {
            throw new NotImplementedException();
        }
    }
}
