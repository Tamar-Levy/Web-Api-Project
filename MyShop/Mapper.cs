using AutoMapper;
using DTO;
using Entities;

namespace MyShop
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Order, OrderDTO>();
        }
    }
}
