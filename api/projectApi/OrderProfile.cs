using AutoMapper;
using projectApi.DAL;
using projectApi.Models;
using projectApi.Models.DTO;
using System.Drawing;


namespace projectApi
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<DonorDto, Donor>();
            CreateMap<PresentDto, Present>();
            CreateMap<Present, Donor>();
            CreateMap<UserDto, User>();
            CreateMap<CartDto, Cart>();
            CreateMap<PurchasesDto, Purchases>(); 
            CreateMap<LoginRequestDto, LoginRequest>();
            CreateMap<WinnersDto, Winners>();   



        }
    }
}
