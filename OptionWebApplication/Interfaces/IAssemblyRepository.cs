
using OptionWebApplication.Models;

namespace OptionWebApplication.Interfaces
{
    public interface IAssemblyRepository
    {
        Task<IEnumerable<Assembly>> GetAll();
        Task<Assembly> GetByIdAsync (int id);
        Task<Assembly> GetAssemblyBySerialNumber (int serialnumber);
        bool Add(Assembly assembly);
        bool Update(Assembly assembly);
        bool Delete(Assembly assembly);
        bool Save();
        
    }
}
