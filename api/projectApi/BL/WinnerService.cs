using projectApi.Models;
using projectApi.Models.DTO;
using projectApi.DAL;

namespace projectApi.BL
{
    public class WinnersService : IWinnersService
    {
        private readonly IWinnersDal WinnersDal;

        public WinnersService(IWinnersDal WinnersDal)
        {
            this.WinnersDal = WinnersDal;
        }

        async Task IWinnersService.Add(WinnersDto WinnersDto)
        {
            await WinnersDal.Add(WinnersDto);
        }

        async Task<Winners> IWinnersService.Get(int id)
        {
            return await WinnersDal.Get(id);
        }

        async Task<List<Winners>> IWinnersService.Get()
        {
            return await WinnersDal.Get();
        }
        
    }
}
