using System;
using System.Collections.Generic;
using System.Text;
using ShopOnWheels.Domain.Models.Product;

namespace ShopOnWheels.Services.Builders.QueryBuilders.Product
{
    public interface IProductSearchQueryBuilder : IQueryBuilder<Domain.Models.Product.Product>
    {
        IProductSearchQueryBuilder SetBaseProductInfo(bool asNoTracking = false);
        IProductSearchQueryBuilder SetName(string name);
        IProductSearchQueryBuilder SetManufacturer(string name);
        IProductSearchQueryBuilder SetCategory(Guid? categoryId);

        IProductSearchQueryBuilder SetPrice(double? from, double? to);
    }
}
