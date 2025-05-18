using projectApi.Models.DTO;
using projectApi.Models;

namespace projectApi.DAL
{
    public interface IPresentDal
    {
        Task<Present> Get(int id);
        Task<List<Purchases>> GetPurchases(int id);
        Task<List<Present>> GetPresentByPurhcases();
        Task<List<Present>> GetPresentOrderByPrice();
        Task<List<User>> GetUserDetails(int presentId);
        Task<Winners> ToDraw(int id);
        Task<List<PresentDto>> Search(string name);
        Task<Present> SearchByDonorName(string name);
        Task Add(PresentDto PresentDto);
        Task<List<Present>> Get();
        Task Update(int id, PresentDto? PresentDTO);
        Task Delete(int id);
        Task<string> generateDochHachnasot();
        Task<Present> SearchByNumOfPurch(int num);
        List<Present> GetDrawnPresents();

    }
}
