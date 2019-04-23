using ShopOnWheels.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopOnWheels.Domain.Models.ProductList
{
    public class ProductList : BaseGuidEntity
    {
        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }
        [ForeignKey("Order")]
        public Guid? OrderId { get; set; }

        public virtual Product.Product Product { get; set; }
        public virtual Order.Order Order { get; set; }
    }
}
