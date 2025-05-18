using projectApi.Models;
using projectApi.Models.DTO;

namespace projectApi.DAL
{
    public interface IPurchasesDal
    {
        Task<Purchases> Get(int id);

        Task Add(PurchasesDto PurchasesCartDto);

        Task<List<Purchases>> Get();
        Task Update(int id, PurchasesDto? PurchasesCartDTO);
        Task Delete(int id);
    }
}
