using Microsoft.EntityFrameworkCore;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;

namespace OptionWebApplication.Repository
{
    public class SertificationRepository : ISertificationRepository
    {
        private readonly ApplicationDbContext _context;
        public SertificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Sertification sertification)
        {
            _context.Add(sertification);
            return Save();
        }

        public bool Delete(Sertification sertification)
        {
            _context.Remove(sertification);
            return Save();
        }

        public async Task<IEnumerable<Sertification>> GetAll()
        {
            return await _context.Sertifications.ToListAsync();
        }

        public async Task<Sertification> GetByIdAsync(int id)
        {
            return await _context.Sertifications.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Sertification sertification)
        {
            _context.Update(sertification);
            return Save();
        }
    }
}
