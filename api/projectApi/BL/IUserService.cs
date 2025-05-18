using projectApi.Models;
using projectApi.Models.DTO;

namespace projectApi.BL
{
    public interface IUserService
    {
        Task<User> Get(int id);

        Task Add(UserDto UserDto);

        Task<List<User>> Get();
        Task Update(int id, UserDto? UserDto);
        Task Delete(int id);
    }
}
