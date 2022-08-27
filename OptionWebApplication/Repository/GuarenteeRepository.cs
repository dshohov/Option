using Microsoft.EntityFrameworkCore;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;

namespace OptionWebApplication.Repository
{
    public class GuarenteeRepository : IGuarenteeRepository
    {
        private readonly ApplicationDbContext _context;
        public GuarenteeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Guarentee guarentee)
        {
            _context.Add(guarentee);
            return Save();
        }

        public bool Delete(Guarentee guarentee)
        {
            _context.Remove(guarentee);
            return Save();
        }

        public async Task<IEnumerable<Guarentee>> GetAll()
        {
            return await _context.Guarentes.ToListAsync();
        }

        public async Task<Guarentee> GetByIdAsync(int id)
        {
            return await _context.Guarentes.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Guarentee> GetGuarenteeBySerialNumber(int serialnumber)
        {
            return await _context.Guarentes.FirstOrDefaultAsync(i => i.SerialNumber == serialnumber);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Guarentee guarentee)
        {
            _context.Update(guarentee);
            return Save();
        }
    }
}
