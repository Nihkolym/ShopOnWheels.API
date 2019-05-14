using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ShopOnWheels.Domain;
using ShopOnWheels.Domain.Models.Product;
using System.Text.RegularExpressions;

namespace ShopOnWheels.Services.Builders.QueryBuilders.Product
{
    public class ProductSearchQueryBuilder : IProductSearchQueryBuilder
    {
        private ShopOnWheelsDbContext _context;
        private IQueryable<Domain.Models.Product.Product> _query;

        public static Func<ShopOnWheelsDbContext, IEnumerable<Domain.Models.Product.Product>> SetBaseMenuItemsInfoCompiledQuery = EF.CompileQuery((ShopOnWheelsDbContext c) =>
            
            c.Products.Include(p => p.Category).Where(o => !o.IsDeleted));

        public IProductSearchQueryBuilder SetBaseProductInfo(bool asNoTracking = false)
        {
            _query = SetBaseMenuItemsInfoCompiledQuery(_context).AsQueryable();

            if (asNoTracking)
            {
                _query = _query.AsNoTracking();
            }

            return this;
        }

        public ProductSearchQueryBuilder(ShopOnWheelsDbContext context)
        {
            _context = context;
        }

        public IQueryable<Domain.Models.Product.Product> Build()
        {
            IQueryable<Domain.Models.Product.Product> resultQuery = _query;
            _query = null;
            return resultQuery;
        }

        public IProductSearchQueryBuilder SetManufacturer(string manufacturer)
        {
            if (!string.IsNullOrEmpty(manufacturer))
            {
                Regex regex = new Regex($"{manufacturer}\\w*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                _query = _query.Where(p => regex.IsMatch(p.Manufacturer));
            }

            return this;
        }

        public IProductSearchQueryBuilder SetName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Regex regex = new Regex($"{name}\\w*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                _query = _query.Where(p => regex.IsMatch(p.Name));
            }

            return this;
        }

        public IProductSearchQueryBuilder SetCategory(Guid? categoryId)
        {
            if (categoryId.HasValue)
            {
                _query = _query.Where(p => p.Category.Id == categoryId);
            }

            return this;
        }

        public IProductSearchQueryBuilder SetPrice(double? from, double? to)
        {
            if (from.HasValue && to.HasValue && from.Value <= to.Value)
            {
                _query = _query.Where(p => p.Price >= from && p.Price <= to);
            }
            else if (from.HasValue)
            {
                _query = _query.Where(p => p.Price >= from);
            }
            else if (to.HasValue)
            {
                _query = _query.Where(p => p.Price <= to);
            }

            return this;
        }
    }
}
