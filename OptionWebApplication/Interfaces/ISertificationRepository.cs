using OptionWebApplication.Models;

namespace OptionWebApplication.Interfaces
{
    public interface ISertificationRepository
    {
        Task<IEnumerable<Sertification>> GetAll();
        Task<Sertification> GetByIdAsync(int id);
        bool Add (Sertification sertification);
        bool Update (Sertification sertification);
        bool Delete (Sertification sertification);
        bool Save(); 
    }
}
