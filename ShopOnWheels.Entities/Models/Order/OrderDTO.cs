using ShopOnWheels.Entities.Models.Base;
using ShopOnWheels.Entities.Models.Product;
using ShopOnWheels.Entities.Models.ProductList;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnWheels.Entities.Models.Order
{
    public class OrderDTO : BaseGuidDTOEntity
    {
        public DateTime OrderDate { get; set; }

        public DateTime OrderDeliver { get; set; }

        public int? Frequency { get; set; }

        public double? Total { get; set; }

        public bool? IsActive { get; set; }
        public string UserId { get; set; }

        public virtual List<ProductDTO> Products { get; set; }
    }
}
