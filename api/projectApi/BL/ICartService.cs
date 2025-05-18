using projectApi.Models;
using projectApi.Models.DTO;

namespace projectApi.BL
{
    public interface ICartService
    {
        Task<Cart> Get(int id);

        Task<int> Add(CartDto CartDTO);

        Task<List<Cart>> Get();
        Task Update(int id, CartDto? CartDTO);
        Task Delete(int id);
        Task<List<Purchases>> GetPurchasesList(int id);
        Task<string> AddPurchase(Purchases purchase);
    }
}
