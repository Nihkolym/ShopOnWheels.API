using AutoMapper;
using ShopOnWheels.Domain.Models.Order;
using ShopOnWheels.Domain.Models.Product;
using ShopOnWheels.Domain.Models.ProductList;
using ShopOnWheels.Entities.Models.Order;
using ShopOnWheels.Entities.Models.Product;
using ShopOnWheels.Entities.Models.ProductList;
using System.Collections.Generic;
using System.Linq;

namespace ShopOnWheels.Services.MapProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>()
                .ForMember(od => od.Products, opt => opt
                    .MapFrom(o => o.ProductList.Select(y => y.Product).ToList())).ReverseMap();

        }
    }
}