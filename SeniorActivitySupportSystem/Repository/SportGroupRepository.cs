using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorActivitySupportSystem.Data;
using SeniorActivitySupportSystem.Interfaces;
using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.Repository
{
    public class SportGroupRepository : ISportGroupRepository
    {
        private readonly ApplicationDbContext _context;
        public SportGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(SportGroup sportGroup)
        {
            _context.Add(sportGroup);
            return Save();
        }

        public bool Delete(SportGroup sportGroup)
        {
            _context.Remove(sportGroup);
            return Save();
        }

        public async Task<IEnumerable<SportGroup>> GetAll()
        {
            return await _context.SportGroups.ToListAsync();
        }

        public async Task<SportGroup> GetByIdAsync(int id)
        {
            return await _context.SportGroups.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<SportGroup> GetByIdAsyncNoTracking(int id)
        {
            return await _context.SportGroups.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<SportGroup>> GetSportGroupByCity(string city)
        {
            return await _context.SportGroups.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(SportGroup sportGroup)
        {
            _context.Update(sportGroup);
            return Save();
        }

    }
}
