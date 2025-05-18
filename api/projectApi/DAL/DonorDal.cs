using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectApi.Models.DTO;
using projectApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace projectApi.DAL
{
     public class DonorDal(OrdersDBContex ordersDBContex, IMapper mapper) : IDonorDal
    {
        private readonly OrdersDBContex ordersDBContex = ordersDBContex ?? throw new ArgumentNullException(nameof(ordersDBContex));
        private readonly IMapper mapper = mapper;

        public async Task<List<Donor>> Get()
        {
            return await ordersDBContex.Donor
                 .Include(d => d.Donations)
                .ToListAsync();
        }
        public async Task Add(DonorDto DonorDto)
        {
            var Donor = mapper.Map<Donor>(DonorDto);
            ordersDBContex.Donor.Add(Donor);

            try
            {
                await ordersDBContex.SaveChangesAsync();
                Console.WriteLine(Donor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving to database", ex);
            }
        }

        public async Task<Donor> Get(int id)
        {
            var Donor = await ordersDBContex.Donor
                .Include(d => d.Donations)
               .FirstOrDefaultAsync(c => c.Id == id);

            if (Donor == null)
            {
                throw new KeyNotFoundException("Donor with id not exists");
            }
            else return Donor;
        
        }

        public async Task<Donor> SearchByName(string name)
        {
            var Donor = await ordersDBContex.Donor
                .Include(d => d.Donations)
               .FirstOrDefaultAsync(c => c.Name == name);

            if (Donor == null)
            {
                throw new KeyNotFoundException("Donor with id not exists");
            }
            else return Donor;
        }

        public async Task<Donor> SearchByEmail(string email)
        {
            var Donor = await ordersDBContex.Donor
                .Include(d => d.Donations)
               .FirstOrDefaultAsync(c => c.Email == email);

            if (Donor == null)
            {
                throw new KeyNotFoundException("Donor with id not exists");
            }
            else return Donor;
        }
        public async Task<Donor> SearchByPresent(string PresentName)
        {
            var Donor = await ordersDBContex.Donor
                .Include(d => d.Donations) // טוען את רשימת המתנות עבור כל תורם
                .FirstOrDefaultAsync(d => d.Donations.Any(p => p.Name == PresentName)); // בודק אם יש מתנה בשם המבוקש

        
            if (Donor == null)
            {
                throw new KeyNotFoundException("Donor with id not exists");
            }
            else return Donor;
        }

        public async Task Update(int id, DonorDto? DonorDto)
        {
            if (DonorDto == null)
            {
                throw new KeyNotFoundException("Donor with id not exists");
            }
            else
            {
                var donor = await ordersDBContex.Donor.FirstOrDefaultAsync(c => c.Id == id);
                if (DonorDto.Name != null && DonorDto.Name != "string")
                    donor.Name = DonorDto.Name;
                if (DonorDto.Email != null && DonorDto.Email != "string")
                    donor.Email = DonorDto.Email;
                if (DonorDto.Phone != null && DonorDto.Phone != "string")
                    donor.Phone = DonorDto.Phone;
                try
                {
                    await ordersDBContex.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error saving to database", ex);
                }
            }
        }
        public async Task Delete(int id)
        {
            var donor = await ordersDBContex.Donor.FirstOrDefaultAsync(c => c.Id == id);
            ordersDBContex.Donor.Remove(donor);
            try
            {
                await ordersDBContex.SaveChangesAsync();
                Console.WriteLine(donor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving to database", ex);
            }
        }

    }
}

