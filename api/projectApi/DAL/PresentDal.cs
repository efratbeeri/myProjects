using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using projectApi.Models.DTO;
using Org.BouncyCastle.Bcpg;
using DocumentFormat.OpenXml.InkML;

namespace projectApi.DAL 
{ 
 public class PresentDal(OrdersDBContex ordersDBContex, IMapper mapper) : IPresentDal
{
    private readonly OrdersDBContex ordersDBContex = ordersDBContex ?? throw new ArgumentNullException(nameof(ordersDBContex));
    private readonly IMapper mapper = mapper;

    public async Task<List<Present>> Get()
    {
        return await ordersDBContex.Present.ToListAsync();
    }
    public async Task Add(PresentDto presentDto)
    {
        var donor = await ordersDBContex.Donor.FindAsync(presentDto.DonorsId) ?? throw new Exception("Donor not found");
        var present = mapper.Map<Present>(presentDto);
            present.Donorname = donor.Name;
        // הוסף את המתנה לרשימת המתנות של התורם
        donor?.Donations.Add(present);
        ordersDBContex.Present.Add(present);
        try
        {
            await ordersDBContex.SaveChangesAsync();
            Console.WriteLine(present);
        }
        catch (Exception ex)
        {
            throw new Exception("Error saving to database", ex);
        }
    }
    public async Task<Present> Get(int id)
    {

            var Present = await ordersDBContex.Present.FirstOrDefaultAsync(c => c.Id == id);           
          
        if (Present == null)
        {
            throw new KeyNotFoundException("present with id not exists");
        }
        else return Present;
    }

        public async Task<List<PresentDto>> Search(string name)
        {
            try
            {
                var presents = await ordersDBContex.Present
                            .Where(p => p.Name.Contains(name))
                            .Select(p => new PresentDto
                            {
                                Name = p.Name,
                                Price = p.Price,
                                Kategory = p.Kategory,
                                Image = p.Image,
                                DonorsId = p.DonorsId
                            })
                    .ToListAsync();

                if (!presents.Any())
                {
                    throw new KeyNotFoundException($"No presents found with the name '{name}'.");
                }

                return presents;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while searching for presents.", ex);
            }
        }


        public async Task<Present> SearchByDonorName(string name)
        {
            var present = await ordersDBContex.Present.FirstOrDefaultAsync(c => c.Donor.Name.Contains(name));
            if (present == null)
            {
                throw new KeyNotFoundException("present with id not exists");
            }
            else return present;
        }

        public async Task<Present> SearchByNumOfPurch(int num)
        {
            var numOfPurch = await ordersDBContex.Purchases.FirstOrDefaultAsync(p => p.Amount.Equals(num));
            if (numOfPurch==null)
            {
                throw new KeyNotFoundException("purch not exists");
            }

            var present = await ordersDBContex.Present.FirstOrDefaultAsync(c => c.Id== numOfPurch.Id);
            if (present == null)
            {
                throw new KeyNotFoundException("present with id not exists");
            }
            else return present;
        }

        public async Task<List<Present>> GetPresentsByMinPurchases(int minPurchases)
        {
            var presents = await ordersDBContex.Present
                //.Where(p => p.PurchasesList.Sum(purchase => purchase.Amount) >= minPurchases) // מסנן לפי סכום הכמות
                .ToListAsync(); 

            return presents; // מחזיר את רשימת המתנות
        }

        public async Task Update(int id, PresentDto? PresentDto)
    {
        if (PresentDto == null)
        {
            throw new ArgumentNullException(nameof(PresentDto));
        }
        else
        {
            var Present = await ordersDBContex.Present.FirstOrDefaultAsync(c => c.Id == id);
            if (Present == null)
            {
                throw new KeyNotFoundException("present with id not exists");
            }

            if (PresentDto.Name != null && PresentDto.Name != "string")
                Present.Name = PresentDto.Name;
            if (PresentDto.Price != null && PresentDto.Price != 0)
                Present.Price = PresentDto.Price;
            if (PresentDto.Kategory != null && PresentDto.Kategory != "string")
                Present.Kategory = PresentDto.Kategory;
                if (PresentDto.DonorsId != null && PresentDto.DonorsId !=0)
                    Present.DonorsId = PresentDto.DonorsId;
                if (PresentDto.Image != null && PresentDto.Image != "string")
                    Present.Image = PresentDto.Image;
                if (PresentDto.IsDrawn != null && PresentDto.IsDrawn != false)
                    Present.IsDrawn = PresentDto.IsDrawn;
                if (PresentDto.WinnerName != null && PresentDto.WinnerName != "string")
                    Present.WinnerName = PresentDto.WinnerName;

                await ordersDBContex.SaveChangesAsync();
        }
    }
    public async Task Delete(int id)
    {
        var present = await ordersDBContex.Present.FirstOrDefaultAsync(c => c.Id == id);
        if (present == null)
        {
            throw new KeyNotFoundException("present with id not exists");
        }
        ordersDBContex.Present.Remove(present);
        await ordersDBContex.SaveChangesAsync();

    }
        public async Task<List<Purchases>> GetPurchases(int presentId)
        {
            var purchases = await ordersDBContex.Purchases
                .Where(p => p.PresentID == presentId)  
                .Where(p=>p.Cart.Final==true)
                .Include(p => p.Present)  
                .ToListAsync()
                ?? throw new Exception("purchases not found for this present");

            return purchases;
        }

        public async Task<List<User>> GetUserDetails(int presentId)
        {
            var cartIdd=await ordersDBContex.Purchases
                .Where(p=>p.PresentID==presentId)
                .Select(p=>p.CartId)
                 .Distinct()
                .ToListAsync();

            var users = await ordersDBContex.Cart
                .Where(c=>cartIdd.Contains(c.Id))
                .Include(p => p.User)
                .Select(c=>c.User)
                .ToListAsync()
                ?? throw new Exception("purchases not found for this present");
           
            var user = new List< User>();

            return user;
        }

        public async Task<List<Present>> GetPresentOrderByPrice()
        {
            var presents=await ordersDBContex.Present
                    .OrderByDescending(p =>p.Price)
                    .ToListAsync();
            return presents;
        }

        public async Task<List<Present>> GetPresentByPurhcases()
        {
            var presents = await ordersDBContex.Present
                    .OrderByDescending(p => ordersDBContex.Purchases
                    .Where(p=>p.PresentID==p.Id)
                    .Sum(p=>p.Amount))
                    .ToListAsync();
            return presents;
        }
        public async Task<Winners> ToDraw(int id)
        {
            var present = await ordersDBContex.Present.FirstOrDefaultAsync(p => p.Id == id);
            if (present == null)
            {
                throw new Exception($"Present with id {id} not found.");
            }

            if (present.IsDrawn)
            {
                throw new Exception($"Present with id {id} has already been drawn.");
            }

            var winId = 0;
            List<int> carts = new List<int>();
            var purchases = await GetPurchases(id);
            foreach (var purchase in purchases)
            {
                for (int i = 0; i < purchase.Amount; i++)
                {
                    carts.Add(purchase.CartId);
                }
            }

            Random random = new Random();
            var randomIndex = random.Next(0, carts.Count);
            winId = (await ordersDBContex.Cart.FirstOrDefaultAsync(p => p.Id == carts[randomIndex])).UserId;

            var win = new Winners()
            {
                PresentID = id,
                UserId = winId
            };


            present.IsDrawn = true;
            var user = await ordersDBContex.User.FirstOrDefaultAsync(u => u.Id == win.UserId);
            present.WinnerName = user?.UserName;

            ordersDBContex.Present.Update(present);
            await ordersDBContex.Winners.AddAsync(win);
            await ordersDBContex.SaveChangesAsync();

            return win;
        }

       
    

        public async Task<string> generateDochHachnasot()
        {
            var counts = ordersDBContex.Purchases
                .Sum(p => p.Amount * p.Present.Price);

            var answer = $"סך ההכנסות מהמכירה הינו: {counts}";
            return answer;
        }

        public List<Present> GetDrawnPresents()
        {
            return ordersDBContex.Present
                .Where(p => p.IsDrawn)
                .ToList();
        }

    }
}
