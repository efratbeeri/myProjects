using projectApi.DAL;
using projectApi.Models;
using projectApi.Models.DTO;

namespace projectApi.BL
{
    public class PurchasesService : IPurchasesService
    {
        private readonly IPurchasesDal PurchasesDal;

        public PurchasesService(IPurchasesDal PurchasesDal)
        {
            this.PurchasesDal = PurchasesDal;
        }


        async Task IPurchasesService.Add(PurchasesDto PurchasesDto)
        {
            await PurchasesDal.Add(PurchasesDto);
        }

        async Task<Purchases> IPurchasesService.Get(int id)
        {
            return await PurchasesDal.Get(id);
        }

        async Task<List<Purchases>> IPurchasesService.Get()
        {
            return await PurchasesDal.Get();
        }
        async Task IPurchasesService.Update(int id, PurchasesDto? PurchasesDto)
        {

            await PurchasesDal.Update(id, PurchasesDto);
        }
        async Task IPurchasesService.Delete(int id)
        {
            await PurchasesDal.Delete(id);
        }

    }
}
