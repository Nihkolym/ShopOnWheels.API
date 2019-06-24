using ShopOnWheels.Entities.Models.Base;
using ShopOnWheels.Entities.Models.Order;
using ShopOnWheels.Entities.Models.Product;
using ShopOnWheels.Entities.Models.ProductList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopOnWheels.Entities.Models.Box
{
    public class BoxDTO : BaseGuidDTOEntity
    {
        [Required]
        public int Weight { get; set; }
        public Guid ProductListId { get; set; }
        public virtual ProductDTO Product { get; set; }
        public virtual OrderDTO Order { get; set; }
    }
}
