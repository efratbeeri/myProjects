using projectApi.Models;
using projectApi.Models.DTO;
using projectApi.DAL;

namespace projectApi.BL
{
    public class UserService : IUserService
    {
        private readonly IUseDal UserDal;

        public UserService(IUseDal UserDal)
        {
            this.UserDal = UserDal;
        }


        async Task IUserService.Add(UserDto UserDto)
        {
            await UserDal.Add(UserDto);
        }

        async Task<User> IUserService.Get(int id)
        {
            return await UserDal.Get(id);
        }

        async Task<List<User>> IUserService.Get()
        {
            return await UserDal.Get();
        }
        async Task IUserService.Update(int id, UserDto? UserDto)
        {

            await UserDal.Update(id, UserDto);
        }
        async Task IUserService.Delete(int id)
        {
            await UserDal.Delete(id);
        }

    }
}
