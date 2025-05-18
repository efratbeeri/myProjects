using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using projectApi.Models;
using projectApi.Models.DTO;
using System.Reflection;

namespace projectApi.DAL
{
    public class WinnersDal(OrdersDBContex ordersDBContex, IMapper mapper) : IWinnersDal
    {

        private readonly OrdersDBContex ordersDBContex = ordersDBContex ?? throw new ArgumentNullException(nameof(ordersDBContex));
        private readonly IMapper mapper = mapper;
        public async Task Add(WinnersDto WinnersDto)
        {
            var win = mapper.Map<Winners>(WinnersDto);
            ordersDBContex.Winners.Add(win);
            try
            {
                await ordersDBContex.SaveChangesAsync();
                Console.WriteLine(win);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving to database", ex);
            }
        }

        public async Task<Winners> Get(int id)
        {
            var win = await ordersDBContex.Winners.FirstOrDefaultAsync(c => c.Id == id);
            if (win == null)
            {
                throw new KeyNotFoundException("win with id not exists");
            }
            else return win;
        }

        public async Task<List<Winners>> Get()
        {
            return await ordersDBContex.Winners.ToListAsync();
        }

    

    }
}
