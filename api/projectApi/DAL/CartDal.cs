using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectApi.Models;
using projectApi.Models.DTO;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace projectApi.DAL
{
    public class CartDal(OrdersDBContex ordersDBContex, IMapper mapper) : ICartDal
    {
        private readonly OrdersDBContex ordersDBContex = ordersDBContex ?? throw new ArgumentNullException(nameof(ordersDBContex));
        private readonly IMapper mapper = mapper;

        public async Task<List<Cart>> Get()
        {
            return await ordersDBContex.Cart
                .Include(x => x.PurchasesList)
                .ToListAsync();
        }
        public async Task<int> Add(CartDto CartDto)
        {
            Console.WriteLine($"Add method called: UserId = {CartDto.UserId}, Final = {CartDto.Final}");
            var Cart = mapper.Map<Cart>(CartDto);
            Console.WriteLine($"Creating Cart: UserId = {Cart.UserId}");
            ordersDBContex.Cart.Add(Cart);
            try
            {
                await ordersDBContex.SaveChangesAsync();
                Console.WriteLine(Cart);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving to database", ex);
            }
            return Cart.Id;
        }
        public async Task<Cart> Get(int id)
        {
            var Cart = await ordersDBContex.Cart
                .Include(x => x.PurchasesList)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Cart == null)
            {
                throw new KeyNotFoundException("present with id not exists");
            }
            else return Cart;
        }
        public async Task Update(int id, CartDto? CartDto)
        {
            if (CartDto == null)
            {
                throw new ArgumentNullException(nameof(CartDto));
            }
            else
            {
                var Cart = await ordersDBContex.Cart.FirstOrDefaultAsync(c => c.Id == id);
                if (Cart == null)
                {
                    throw new KeyNotFoundException("Cart with id not exists");
                }

                if (CartDto.Final != null && CartDto.Final != false)
                    Cart.Final = CartDto.Final;
                if (CartDto.UserId != null && CartDto.UserId != 0)
                    Cart.UserId = CartDto.UserId;
          


                await ordersDBContex.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var Cart = await ordersDBContex.Cart.FirstOrDefaultAsync(c => c.Id == id);
            if (Cart == null)
            {
                throw new KeyNotFoundException("Cart with id not exists");
            }
            ordersDBContex.Cart.Remove(Cart);
            await ordersDBContex.SaveChangesAsync();

        }

        //public async Task<List<Purchases>> GetPurchasesList(int id)
        //{
        //    var Cart = await ordersDBContex.Cart.FirstOrDefaultAsync(c => c.Id == id);
        //    return (List<Purchases>)Cart.PurchasesList;

        //}
        public async Task<List<Purchases>> GetPurchasesList(int cartId)
        {
            // שימוש ב-Include כדי לטעון את הרשימה המקושרת
            var cart = await ordersDBContex.Cart
                .Include(c => c.PurchasesList) // טעינת הרשימה המקושרת
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null || cart.PurchasesList == null)
            {
                return new List<Purchases>(); // אם אין סל או הרשימה ריקה, החזר רשימה ריקה
            }

            return cart.PurchasesList.ToList(); // המרת ICollection ל-List
        }


        public async Task<string> AddPurchase(Purchases purchase)
        {
            var cart = await ordersDBContex.Cart.FirstOrDefaultAsync(c => c.Id == purchase.CartId);
            if (cart == null)
            {
                return "Cart not found";
            }

            cart.PurchasesList.Add(purchase);
            await ordersDBContex.SaveChangesAsync();
            return "add purchases succesfully";
        }


    }
}
