using AutoMapper;
using projectApi.DAL;
using projectApi.Models;
using projectApi.Models.DTO;

namespace projectApi.BL
{
    public class CartService : ICartService
    {
        private readonly ICartDal CartDal;

        public CartService(ICartDal CartDal)
        {
            this.CartDal = CartDal;
        }


        async Task<int> ICartService.Add(CartDto CartDto)
        {
            Console.WriteLine($"Add method called: UserId = {CartDto.UserId}, Final = {CartDto.Final}");

            return await CartDal.Add(CartDto);
        }


        async Task<Cart> ICartService.Get(int id)
        {
            return await CartDal.Get(id);
        }

        async Task<List<Cart>> ICartService.Get()
        {
            return await CartDal.Get();
        }
        async Task ICartService.Update(int id, CartDto? CartDto)
        {

            await CartDal.Update(id, CartDto);
        }
        async Task ICartService.Delete(int id)
        {
            await CartDal.Delete(id);
        }

        async Task<List<Purchases>> ICartService.GetPurchasesList(int id)
        {
            return await CartDal.GetPurchasesList(id);
        }

        async Task<string> ICartService.AddPurchase(Purchases purchase)
        {
            return await CartDal.AddPurchase(purchase);
        }
    }
}
