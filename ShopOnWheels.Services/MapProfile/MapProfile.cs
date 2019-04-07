using AutoMapper;
using ShopOnWheels.Domain.Models.Order;
using ShopOnWheels.Entities.Models.Product;

namespace ShopOnWheels.Services.MapProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}