using Newtonsoft.Json;
using ShopOnWheels.Entities.Models.Order;
using ShopOnWheels.Entities.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnWheels.Entities.Models.ProductList
{
    public class ProductListDTO
    {
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }

        public virtual OrderDTO OrderDTO{ get; set; }
        public virtual ProductDTO ProductDTO { get; set; }
    }
}
