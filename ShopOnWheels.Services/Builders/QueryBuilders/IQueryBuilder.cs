using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopOnWheels.Services.Builders.QueryBuilders
{
    public interface IQueryBuilder<TEntity>
    {
        IQueryable<TEntity> Build();
    }
}
