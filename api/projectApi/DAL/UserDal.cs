using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using projectApi.Models.DTO;

namespace projectApi.DAL
{
    public class UserDal(OrdersDBContex ordersDBContex, IMapper mapper) : IUseDal
    {
        private readonly OrdersDBContex ordersDBContex = ordersDBContex ?? throw new ArgumentNullException(nameof(ordersDBContex));
        private readonly IMapper mapper = mapper;

        public async Task<List<User>> Get()
        {
            return await ordersDBContex.User.ToListAsync();
        }
        
        public async Task Add(UserDto UserDto)
        {
            string hashPassword=BCrypt.Net.BCrypt.HashPassword(UserDto.Password);
            var User = mapper.Map<User>(UserDto);
            User.Password = hashPassword;
            ordersDBContex.User.Add(User);
            try
            {
                await ordersDBContex.SaveChangesAsync();
                Console.WriteLine(User);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving to database", ex);
            }
        }
        public async Task<User> Get(int id)
        {
            var User = await ordersDBContex.User.FirstOrDefaultAsync(c => c.Id == id);
            if (User == null)
            {
                throw new KeyNotFoundException("User with id not exists");
            }
            else return User;
        }
        public async Task Update(int id, UserDto? UserDto)
        {
            if (UserDto == null)
            {
                throw new ArgumentNullException(nameof(UserDto));
            }
            else
            {
                var User = await ordersDBContex.User.FirstOrDefaultAsync(c => c.Id == id);
                if (User == null)
                {
                    throw new KeyNotFoundException("User with id not exists");
                }

                if (UserDto.Name != null && UserDto.Name != "string")
                    User.Name = UserDto.Name;
                if (UserDto.Email != null && UserDto.Email != "string")
                    User.Email = UserDto.Email;
                if (UserDto.Phone != null && UserDto.Phone != "string")
                    User.Phone = UserDto.Phone;
                if (UserDto.UserName != null && UserDto.UserName != "string")
                    User.UserName = UserDto.UserName;
                if (UserDto.Password != null && UserDto.Password != "string")
                    User.Password = UserDto.Password;


                await ordersDBContex.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var User = await ordersDBContex.Winners.FirstOrDefaultAsync(c => c.Id == id);
            if (User == null)
            {
                throw new KeyNotFoundException("User with id not exists");
            }
            ordersDBContex.Winners.Remove(User);
            await ordersDBContex.SaveChangesAsync();

        }

    }
}
