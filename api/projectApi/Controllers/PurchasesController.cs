using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using projectApi.Models.DTO;
using projectApi.Models;
using projectApi.BL;

namespace projectApi.Controllers
{
    //[Authorize(Roles = "manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController(IPurchasesService PurchasesService) : ControllerBase
    {
        private readonly IPurchasesService PurchasesService = PurchasesService;


        [HttpGet]
        public async Task<List<Purchases>> Get()
        {
            Log.Information("Getting Purchases");
            try
            {
                return await PurchasesService.Get();
            }
            catch (Exception ex)
            {
                throw new Exception("Error get of database", ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<Purchases> Get(int id, IPurchasesService PurchasesService)
        {
            Log.Information("GettingByID Purchases");

            try
            {
                return await PurchasesService.Get(id);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error getByIDPurchases of database", ex);
            }
        }

        [HttpPost]
        public async Task Add([FromBody] PurchasesDto value)
        {
            Log.Information("post Purchases");
            try
            {
                await PurchasesService.Add(value);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding Purchases");
                throw new Exception("Error add to database", ex);
            }
        }


        // PUT api/<PurchasesController>/5
        [HttpPut("{id}")]
        public async Task UpdateAsync(int id, PurchasesDto PurchasesDto)
        {
            Log.Information("put Purchases");
            try
            {
                await PurchasesService.Update(id, PurchasesDto);
                Log.Information($"Purchases with Id {id} updated successfully");
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error($"Purchases with Id {id} not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error($"Error updating Purchases with Id {id}: {ex.Message}");
                throw;
            }
        }



        // DELETE api/<PurchasesController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            Log.Information("delete Purchases");
            try
            {
                await PurchasesService.Delete(id);
                Log.Information($"Purchases with Id {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error($"Purchases with Id {id} not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error($"Error deleting Purchases with Id {id}: {ex.Message}");
                throw;
            }

        }
    }
}
