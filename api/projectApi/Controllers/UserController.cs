using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using projectApi.BL;
using projectApi.Models.DTO;
using projectApi.Models;

namespace projectApi.Controllers
{
    //[Authorize(Roles = "manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService UserService) : ControllerBase
    {
        private readonly IUserService UserService = UserService;



        [HttpGet]
        public async Task<List<User>> Get()
        {
            Log.Information("Getting User");
            try
            {
                return await UserService.Get();
            }
            catch (Exception ex)
            {
                throw new Exception("Error get of database", ex);
            }
        }

        // GET api/<DonorController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id, IUserService UserService)
        {
            Log.Information("GettingByID User");

            try
            {
                return await UserService.Get(id);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error getByIDUser of database", ex);
            }
        }




        [HttpPost]
        public async Task Add([FromBody] UserDto value)
        {
            Log.Information("post User");
            try
            {
                await UserService.Add(value);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding User");
                throw new Exception("Error add to database", ex);
            }
        }


        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task Update(int id, UserDto UserDTO)
        {
            Log.Information("put User");
            try
            {
                await UserService.Update(id, UserDTO);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error($"User with Id {id} not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error($"Error update User with Id {id}: {ex.Message}");
                throw;
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            Log.Information("delete User");
            try
            {
                await UserService.Delete(id);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error($"User with Id {id} not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error($"Error deleting User with Id {id}: {ex.Message}");
                throw;
            }

        }
    }
}
