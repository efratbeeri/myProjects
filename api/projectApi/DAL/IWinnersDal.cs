using projectApi.Models.DTO;
using projectApi.Models;

namespace projectApi.DAL
{
    public interface IWinnersDal
    {
        Task<Winners> Get(int id);

        Task Add(WinnersDto WinnersDto);

        Task<List<Winners>> Get();
    }
}
