using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using System.Collections.Generic;
using projectApi.Models.DTO;
using projectApi.Models;
using projectApi.BL;
using System.Security.Claims;

namespace projectApi.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController(IDonorService DonorService) : ControllerBase
    {
        private readonly IDonorService DonorService = DonorService;

        [HttpGet]
        public async Task<List<Donor>> Get()
        {
                Log.Information("Getting Donor");
            try
            {
                return await DonorService.Get();
            }
            catch (Exception ex)
            {
                throw new Exception("Error get of database", ex);
            }
        }

        // GET api/<DonorController>/5
        [HttpGet("{id}")]
        public async Task<Donor> Get(int id, IDonorService DonorService)
        {
            Log.Information("GettingByID Donor");

            try
            {
                return await DonorService.Get(id);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error getByIDDonor of database", ex);
            }
        }

        [HttpGet("searchByName/{name}")]
        public async Task<Donor> SearchByName(string name, IDonorService DonorService)
        {
            Log.Information("searchByName Donor");

            try
            {
                return await DonorService.SearchByName(name);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error SearchByName of database", ex);
            }
        }

        [HttpGet("searchByEmail/{email}")]
        public async Task<Donor> SearchByEmail(string email, IDonorService DonorService)
        {
            Log.Information("searchByName Donor");

            try
            {
                return await DonorService.SearchByEmail(email);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error SearchByEmail of database", ex);
            }
        }


        [HttpGet("searchByPresent/{PresentName}")]
        public async Task<Donor> SearchByPresent(string PresentName, IDonorService DonorService)
        {
            Log.Information("SearchDonorByPresent Donor");

            try
            {
                return await DonorService.SearchByPresent(PresentName);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error SearchDonorByPresent of database", ex);
            }
        }

        [HttpPost]
        public async Task Add([FromBody] DonorDto value)
        {
            Log.Information("post Donor");
            try
            {
                await DonorService.Add(value);
                //return Ok(); // או אפשר להחזיר StatusCode(201) אם רוצים להראות שהרשומה נוספה
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding donor");
                //return StatusCode(500, "Internal server error");
                throw new Exception("Error add to database", ex);
            }
        }


        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task Update(int id, DonorDto DonorDto)
        {
            Log.Information("put Donor");
            try
            {
                await DonorService.Update(id, DonorDto);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error($"Donor with Id {id} not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error($"Error update Donor with Id {id}: {ex.Message}");
                throw;
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            Log.Information("delete Donor");
            try
            {
                await DonorService.Delete(id);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error($"Donor with Id {id} not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error($"Error deleting Donor with Id {id}: {ex.Message}");
                throw;
            }

        }
    }
}

