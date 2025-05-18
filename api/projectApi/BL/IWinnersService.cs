using projectApi.Models;
using projectApi.Models.DTO;

namespace projectApi.BL
{
    public interface IWinnersService
    {
        Task<Winners> Get(int id);

        Task Add(WinnersDto WinnersDTO);

        Task<List<Winners>> Get();



    }
}
