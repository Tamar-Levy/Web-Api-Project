using AutoMapper;
using DTO;
using Entities;

namespace MyShop
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, GetUserDTO>();
            CreateMap<RegisterUserDTO, User>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderItemDTO, OrderItem>();
            CreateMap<PostOrderDTO, Order>();
        }
    }
}
