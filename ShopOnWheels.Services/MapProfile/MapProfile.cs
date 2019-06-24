using AutoMapper;
using ShopOnWheels.Domain.Models.Box;
using ShopOnWheels.Domain.Models.Category;
using ShopOnWheels.Domain.Models.Order;
using ShopOnWheels.Domain.Models.Product;
using ShopOnWheels.Domain.Models.ProductList;
using ShopOnWheels.Entities.Models.Box;
using ShopOnWheels.Entities.Models.Category;
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

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Box, BoxDTO>()
                  .ForMember(b => b.Product, opt => opt
                  .MapFrom(pl => pl.ProductList.Product))
                  .ForMember(b => b.Order, opt => opt
                  .MapFrom(pl => pl.ProductList.Order))
                  .ReverseMap();

        }
    }
}