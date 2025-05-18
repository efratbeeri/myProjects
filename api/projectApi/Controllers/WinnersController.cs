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
    public class WinnersController(IWinnersService WinnersService) : ControllerBase
    {
        private readonly IWinnersService WinnersService = WinnersService;



        [HttpGet]
        public async Task<List<Winners>> Get()
        {
            Log.Information("Getting Winners");
            try
            {
                return await WinnersService.Get();
            }
            catch (Exception ex)
            {
                throw new Exception("Error get of database", ex);
            }
        }

      

        // GET api/<DonorController>/5
        [HttpGet("{id}")]
        public async Task<Winners> Get(int id, IWinnersService WinnersService)
        {
            Log.Information("GettingByID Winners");

            try
            {
                return await WinnersService.Get(id);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error getByIDWinners of database", ex);
            }
        }




        [HttpPost]
        public async Task Add([FromBody] WinnersDto value)
        {
            Log.Information("add Winners");
            try
            {
                await WinnersService.Add(value);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding Winners");
                throw new Exception("Error add to database", ex);
            }
        }
    }
}
