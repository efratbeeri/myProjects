using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectApi.Models;
using projectApi.Models.DTO;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace projectApi.DAL
{
    public class PurchasesDal(OrdersDBContex ordersDBContex, IMapper mapper) : IPurchasesDal
    {
        private readonly OrdersDBContex ordersDBContex = ordersDBContex ?? throw new ArgumentNullException(nameof(ordersDBContex));
        private readonly IMapper mapper = mapper;

        public async Task<List<Purchases>> Get()
        {
            return await ordersDBContex.Purchases.ToListAsync();
        }
        public async Task Add(PurchasesDto PurchasesDto)
        {
            var cart = await ordersDBContex.Cart.FindAsync(PurchasesDto.CartId) ?? throw new Exception("cart not found");
            var Purchases = mapper.Map<Purchases>(PurchasesDto);
            cart?.PurchasesList.Add(Purchases);
            ordersDBContex.Purchases.Add(Purchases);
            try
            {
                await ordersDBContex.SaveChangesAsync();
                Console.WriteLine(Purchases);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving to database", ex);
            }
        }
        public async Task<Purchases> Get(int id)
        {
            var Purchases = await ordersDBContex.Purchases.FirstOrDefaultAsync(c => c.Id == id);
            if (Purchases == null)
            {
                throw new KeyNotFoundException("Purchases with id not exists");
            }
            else return Purchases;
        }
        public async Task Update(int id, PurchasesDto? PurchasesDto)
        {
            if (PurchasesDto == null)
            {
                throw new ArgumentNullException(nameof(PurchasesDto));
            }
            else
            {
                var Purchases = await ordersDBContex.Purchases.FirstOrDefaultAsync(c => c.Id == id);
                if (Purchases == null)
                {
                    throw new KeyNotFoundException("Purchases with id not exists");
                }

                if (PurchasesDto.Amount != null && PurchasesDto.Amount != 0)
                    Purchases.Amount = PurchasesDto.Amount;
                if (PurchasesDto.CartId != null && PurchasesDto.CartId != 0)
                    Purchases.CartId = PurchasesDto.CartId;
                if (PurchasesDto.PresentID != null && PurchasesDto.PresentID != 0)
                    Purchases.PresentID = PurchasesDto.PresentID;

                await ordersDBContex.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var Purchases = await ordersDBContex.Purchases.FirstOrDefaultAsync(c => c.Id == id);
            if (Purchases == null)
            {
                throw new KeyNotFoundException("Purchases with id not exists");
            }
            ordersDBContex.Purchases.Remove(Purchases);
            await ordersDBContex.SaveChangesAsync();

        }

    }
}
