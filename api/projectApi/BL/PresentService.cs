using projectApi.DAL;
using projectApi.Models;
using projectApi.Models.DTO;

namespace projectApi.BL
{
    public class PresentService : IPresentService
    {
        private readonly IPresentDal PresentDal;

        public PresentService(IPresentDal PresentDal)
        {
            this.PresentDal = PresentDal;
        }


        async Task IPresentService.Add(PresentDto PresentDto)
        {
            await PresentDal.Add(PresentDto);
        }

        async Task<Present> IPresentService.Get(int id)
        {
            return await PresentDal.Get(id);
        }

        async Task<Present> IPresentService.SearchByDonorName(string name)
        {
            return await PresentDal.SearchByDonorName(name);
        }

        async Task<List<PresentDto>> IPresentService.Search(string name)
        {
            return await PresentDal.Search(name);
        }
        
        async Task<List<Present>> IPresentService.Get()
        {
            return await PresentDal.Get();
        }
        async Task IPresentService.Update(int id, PresentDto? PresentDto)
        {

            await PresentDal.Update(id, PresentDto);
        }
        async Task IPresentService.Delete(int id)
        {
            await PresentDal.Delete(id);
        }

        async Task<List<Purchases>> IPresentService.GetPurchases(int id)
        {
           return await PresentDal.GetPurchases(id);
        }
        
        async Task<List<Present>> IPresentService.GetPresentByPurhcases()
        {
            return await PresentDal.GetPresentByPurhcases();
        }

        async Task<Winners> IPresentService.ToDraw(int id)
        {
            return await PresentDal.ToDraw(id);
        }

        async Task<string> IPresentService.generateDochHachnasot()
        {
            return await PresentDal.generateDochHachnasot();  
        }
     

        async Task<Present> IPresentService.SearchByNumOfPurch(int num)
        {
            return await PresentDal.SearchByNumOfPurch(num);
        } 

        async Task<List<Present>> IPresentService.GetPresentOrderByPrice()
        {
            return await PresentDal.GetPresentByPurhcases();
        }

        async Task<List<User>> IPresentService.GetUserDetails(int presentId)
        {
            return await PresentDal.GetUserDetails(presentId);
        }

        List<Present> IPresentService.GetDrawnPresents()
        {
            return PresentDal.GetDrawnPresents();
        }
    }
}
