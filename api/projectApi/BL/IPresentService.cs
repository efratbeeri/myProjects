using projectApi.Models.DTO;
using projectApi.Models;

namespace projectApi.BL
{
    public interface IPresentService
    {
        Task<Present> Get(int id);
        Task<List<PresentDto>> Search(string name);
        Task<Present> SearchByDonorName(string name);
        Task Add(PresentDto PresentDto);
        Task<List<Purchases>> GetPurchases(int id);
        Task<List<Present>> GetPresentByPurhcases();
        Task<Winners> ToDraw(int id);
        Task<List<Present>> Get();
        Task Update(int id, PresentDto? PresentDto);
        Task Delete(int id);
        Task<string> generateDochHachnasot();
        Task<Present> SearchByNumOfPurch(int num);
        Task<List<Present>> GetPresentOrderByPrice();
        Task<List<User>> GetUserDetails(int presentId);
        List<Present> GetDrawnPresents();

    }
}
