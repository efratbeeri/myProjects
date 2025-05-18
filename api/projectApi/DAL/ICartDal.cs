using Microsoft.AspNetCore.Mvc;
using projectApi.Models;
using projectApi.Models.DTO;

namespace projectApi.DAL
{
    public interface ICartDal
    {
        Task<Cart> Get(int id);

        Task<int> Add(CartDto CartDto);

        Task<List<Cart>> Get();
        Task Update(int id, CartDto? CartDto);
        Task Delete(int id);
        Task<List<Purchases>> GetPurchasesList(int id);
        Task<string> AddPurchase(Purchases purchase);
    }
}
