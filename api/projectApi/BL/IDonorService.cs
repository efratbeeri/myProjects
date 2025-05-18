using projectApi.Models.DTO;
using projectApi.Models;

namespace projectApi.BL
{
    public interface IDonorService
    {
        Task<Donor> Get(int id);

        Task Add(DonorDto DonorDto);
        Task<Donor> SearchByName(string name);
        Task<Donor> SearchByEmail(string name);
        Task<Donor> SearchByPresent(string PresentName);
        Task<List<Donor>> Get();
        Task Update(int id, DonorDto? DonorDto);
        Task Delete(int id);
    }
}
