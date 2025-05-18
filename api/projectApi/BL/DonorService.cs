using projectApi.DAL;
using projectApi.Models;
using projectApi.Models.DTO;

namespace projectApi.BL
{
    public class DonorService : IDonorService
    {
        private readonly IDonorDal DonorDal;

        public DonorService(IDonorDal DonorDal)
        {
            this.DonorDal = DonorDal;
        }
        async Task IDonorService.Add(DonorDto DonorDto)
        {
            await DonorDal.Add(DonorDto);
        }

        async Task<Donor> IDonorService.Get(int id)
        {
            return await DonorDal.Get(id);
        }
        async Task<Donor> IDonorService.SearchByEmail(string name)
        {
            return await DonorDal.SearchByEmail(name);
        }
        async Task<Donor> IDonorService.SearchByName(string name)
        {
            return await DonorDal.SearchByName(name);
        }
        async Task<Donor> IDonorService.SearchByPresent(string PresentName)
        {
            return await DonorDal.SearchByPresent(PresentName);
        }

        async Task<List<Donor>> IDonorService.Get()
        {
            return await DonorDal.Get();
        }
        async Task IDonorService.Update(int id, DonorDto? DonorDto)
        {

            await DonorDal.Update(id, DonorDto);
        }
        async Task IDonorService.Delete(int id)
        {
            await DonorDal.Delete(id);
        }

    }
}
