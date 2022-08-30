using Microsoft.EntityFrameworkCore;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;
using System.Linq;
using System.IO;


namespace OptionWebApplication.Repository
{
    public class AssemblyRepository : IAssemblyRepository
    {
        private readonly ApplicationDbContext _context;
        public AssemblyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Assembly assembly)  
        {
            _context.Add(assembly);
            return Save();
        }

        public bool Delete(Assembly assembly)
        {
            _context.Remove(assembly);
            return Save();
        }

        public async Task<IEnumerable<Assembly>> GetAll()
        {
            return await _context.Assemblies.ToListAsync();
        }

        public async Task<Assembly> GetByIdAsync(int id)
        {
            return await _context.Assemblies.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Assembly> GetAssemblyBySerialNumber(int serialnumber)
        {
            
            return await _context.Assemblies.FirstOrDefaultAsync(i => i.SerialNumber == serialnumber);   
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;        
        }

        public bool Update(Assembly assembly)
        {
            _context.Update(assembly);
            return Save();
        }

        
    }
}
