using AutoMapper;
using Repository.DTO;
using Repository.Models;

namespace ClothesWeb.AutoMapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Category, CategoryDTO>().ReverseMap();
                config.CreateMap<Product, ProductDTO>().ReverseMap();
                config.CreateMap<Cart, CartDTO>().ReverseMap();
                config.CreateMap<Order, OrderDTO>().ReverseMap();
                config.CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
                config.CreateMap<User, UserDTO>().ReverseMap();
                config.CreateMap<Discount, DiscountDTO>().ReverseMap();


            });
            return mappingConfig;
        }
    }
}
