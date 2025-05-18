using projectApi.Models;
using projectApi.Models.DTO;

namespace projectApi.BL
{
    public interface IPurchasesService
    {
        Task<Purchases> Get(int id);

        Task Add(PurchasesDto PurchasesDto);

        Task<List<Purchases>> Get();
        Task Update(int id, PurchasesDto? PurchasesDto);
        Task Delete(int id);
    }
}
