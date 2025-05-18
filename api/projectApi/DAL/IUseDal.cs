using projectApi.Models.DTO;
using projectApi.Models;

namespace projectApi.DAL
{
    public interface IUseDal
    {
        Task<User> Get(int id);

        Task Add(UserDto UserDto);

        Task<List<User>> Get();
        Task Update(int id, UserDto? UserDTO);
        Task Delete(int id);
    }
}
