using Microsoft.AspNetCore.Mvc;
using projectApi.BL;
using projectApi.Models.DTO;
using projectApi.Models;
using Serilog;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
    }

    [HttpGet]
    public async Task<List<Cart>> Get()
    {
        Log.Information("Getting Cart");
        try
        {
            return await _cartService.Get();
        }
        catch (Exception ex)
        {
            throw new Exception("Error get of database", ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<Cart> Get(int id)
    {
        Log.Information("GettingByID Cart");

        try
        {
            return await _cartService.Get(id);
        }
        catch (KeyNotFoundException ex)
        {
            throw new Exception("Error getByIDCart of database", ex);
        }
    }
    [HttpGet("purchasesList/{id}")]
    public async Task<List<Purchases>> GetPurchasesList(int id)
    {
        Log.Information("GetPurchasesList ");
        try
        {
            return await _cartService.GetPurchasesList(id);
        }
        catch (KeyNotFoundException ex)
        {
            throw new Exception("Error GetPurchasesList of database", ex);
        }

    }

    [HttpPost]
    public async Task<int> Add([FromBody] CartDto value)
    {
        Log.Information("post Cart");
        try
        {
           var a= await _cartService.Add(value); 
            return a;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error adding Cart");
            throw new Exception("Error add to database", ex);
        }
       
    }
    //[HttpPost("add-purchase")]
    //public async Task<IActionResult> AddPurchase([FromBody] Purchases purchase)
    //{
    //    Log.Information("add purchases");
    //    try
    //    {
    //        await _cartService.AddPurchase(purchase);
    //        Log.Information("add purchases succefully");
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        Log.Error($"error in add purchases to cart");
    //        throw;
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Error($"Error: {ex.Message}");
    //        throw;
    //    }
    //    return Ok(purchase);
    //}
    [HttpPost("add-purchase")]
    public async Task<IActionResult> AddPurchase([FromBody] Purchases purchase)
    {
        Log.Information("add purchases");
        try
        {
            var message = await _cartService.AddPurchase(purchase);
            var updatedCart = await _cartService.Get(purchase.CartId); // שליפת ה-Cart המעודכן
            return Ok(updatedCart); // החזרת ה-Cart המעודכן
        }
        catch (Exception ex)
        {
            Log.Error($"Error adding purchase: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{id}")]
    public async Task UpdateAsync(int id, CartDto CartDto)
    {
        Log.Information("put Cart");
        try
        {
            await _cartService.Update(id, CartDto);
            Log.Information($"Cart with Id {id} updated successfully");
        }
        catch (KeyNotFoundException ex)
        {
            Log.Error($"Cart with Id {id} not found: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Log.Error($"Error updating Cart with Id {id}: {ex.Message}");
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(int id)
    {
        Log.Information("delete Cart");
        try
        {
            await _cartService.Delete(id);
            Log.Information($"Cart with Id {id} deleted successfully");
        }
        catch (KeyNotFoundException ex)
        {
            Log.Error($"Cart with Id {id} not found: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Log.Error($"Error deleting Cart with Id {id}: {ex.Message}");
            throw;
        }
    }
    
}
