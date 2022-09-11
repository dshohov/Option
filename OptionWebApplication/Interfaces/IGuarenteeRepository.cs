using OptionWebApplication.Models;

namespace OptionWebApplication.Interfaces
{
    public interface IGuarenteeRepository
    {
        Task<IEnumerable<Guarentee>> GetAll();
        Task<Guarentee> GetByIdAsync(int id);
        Task<Guarentee> GetGuarenteeBySerialNumber(string serialnumber);
        bool Add(Guarentee guarentee);
        bool Update(Guarentee guarentee);
        bool Delete(Guarentee guarentee);
        bool Save();
        void CreatePdf(Guarentee guarentee);
    }
}
