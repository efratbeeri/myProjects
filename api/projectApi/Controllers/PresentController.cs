using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using projectApi.BL;
using projectApi.Models.DTO;
using projectApi.Models;
using Microsoft.EntityFrameworkCore;
using projectApi.DAL;

namespace projectApi.Controllers
{
    //[Authorize(Roles = "manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class PresentController(IPresentService PresentService, FileHandler fileHandler) : ControllerBase
    {
        private readonly IPresentService PresentService = PresentService;
        private readonly FileHandler _fileHandler=fileHandler;

        [HttpPost("saveWinner")]
        public async Task<IActionResult> SaveWinner([FromBody] Winners request)
        {
            var winners = await _fileHandler.SaveWinnerDocument(request.PresentID, request.UserId);
            return Ok(winners);
        }

        [HttpPost("saveRevenue")]
        public async Task<IActionResult> SaveRevenue()
        {
            var revenue = await _fileHandler.SaveRevenueDocument();
            return Ok(revenue);
        }

        [HttpGet]
        public async Task<List<Present>> Get()
        {
            Log.Information("Getting present");
            try
            {
                return await PresentService.Get();
            }
            catch (Exception ex)
            {
                throw new Exception("Error get of database", ex);
            }
        }
        [HttpPost]
        public async Task Add(PresentDto presentDto)
        {
            Log.Information("add present");
            try
            {
                await PresentService.Add(presentDto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error add present", ex);

            }
        }
            [HttpGet("present/{id}")]
        public async Task<Present> Get(int id, IPresentService PresentService)
        {
            Log.Information("GettingByID Present");

            try
            {
                return await PresentService.Get(id);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error getByIDPresent of database", ex);
            }
        }
        [HttpGet("doch")]
        public async Task<string> generateDochHachnasot()
        {
            Log.Information("generateDochHachnasot");
            try
            {
                return await PresentService.generateDochHachnasot();
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error generateDochHachnasot of database", ex);
            }
        }
       
        [HttpGet("present/search/{name}")]
        public async Task<List<PresentDto>> Search(string name, IPresentService PresentService)
        {
            Log.Information("Search Present");

            try
            {
                return await PresentService.Search(name);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error SearchPresent of database", ex);
            }
        }

        [HttpGet("getPurchases/{id}")]
        public async Task<List<Purchases>> getPurchases(int id, IPresentService PresentService)
        {
            Log.Information("getPurchases");

            try
            {
                return await PresentService.GetPurchases(id);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error SearchPresent of database", ex);
            }
        }

        [HttpGet("getPresentByPurchases")]
        public async Task<List<Present>> getPresentByPurchases(IPresentService PresentService)
        {
            Log.Information("getPresentByPurchases");

            try
            {
                return await PresentService.GetPresentByPurhcases();
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error GetPresentByPurhcases of database", ex);
            }
        }


        [HttpGet("GetPresentOrderByPrice")]
        public async Task<List<Present>> GetPresentOrderByPrice(IPresentService PresentService)
        {
            Log.Information("GetPresentOrderByPrice");

            try
            {
                return await PresentService.GetPresentOrderByPrice();
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error GetPresentOrderByPrice of database", ex);
            }
        }

        [HttpGet("GetUserDetails")]
        public async Task<List<User>> GetUserDetails(int presentId,IPresentService PresentService)
        {
            Log.Information("GetUserDetails");

            try
            {
                return await PresentService.GetUserDetails(presentId);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error GetUserDetails of database", ex);
            }
        }

        [HttpGet("toDraw/{id}" )]
        public async Task<Winners> ToDraw(int id,IPresentService PresentService)
        {
            Log.Information("draw");

            try
            {
                return await PresentService.ToDraw(id);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error SearchPresent of database", ex);
            }
        }


        [HttpGet("searchByDonorName/{name}")]
        public async Task<Present> SearchByDonorName(string name, IPresentService PresentService)
        {
            Log.Information("searchPresentByDonorName");

            try
            {
                return await PresentService.SearchByDonorName(name);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error searchByDonorName of database", ex);
            }
        }


        [HttpGet("SearchByNumOfPurch/{num}")]
        public async Task<Present> SearchByNumOfPurch(int num, IPresentService PresentService)
        {
            Log.Information("SearchByNumOfPurch");

            try
            {
                return await PresentService.SearchByNumOfPurch(num);
            }

            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error SearchByNumOfPurch of database", ex);
            }
        }

        [HttpGet("GetDrawnPresents")]
        public List<Present> GetDrawnPresents()
        {
            Log.Information("GetDrawnPresents ");
            try
            {
                return PresentService.GetDrawnPresents();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error  GetDrawnPresents");
                throw new Exception("Error GetDrawnPresents", ex);
            }
        }


        // PUT api/<PresentController>/5
        [HttpPut("{id}")]
        public async Task UpdateAsync(int id, PresentDto PresentDTO)
        {
            Log.Information("put Present");
            try
            {
                await PresentService.Update(id, PresentDTO);
                Log.Information($"Present with Id {id} updated successfully");
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error($"Present with Id {id} not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error($"Error updating Present with Id {id}: {ex.Message}");
                throw;
            }
        }



        // DELETE api/<PresentController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            Log.Information("delete present");
            try
            {
                await PresentService.Delete(id);
                Log.Information($"Present with Id {id} deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error($"Present with Id {id} not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error($"Error deleting Present with Id {id}: {ex.Message}");
                throw;
            }

        }
    }
}
