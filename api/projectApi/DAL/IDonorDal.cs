using projectApi.Models.DTO;
using projectApi.Models;

namespace projectApi.DAL
{
    public interface IDonorDal
    {

        Task<Donor> Get(int id);

        Task Add(DonorDto donorDto);
        Task<List<Donor>> Get();
        Task<Donor> SearchByEmail(string email);
        Task<Donor> SearchByName(string name);
        Task<Donor> SearchByPresent(string PresentName);
        Task Update(int id, DonorDto? donorsDTO);
        Task Delete(int id);
    }
}
