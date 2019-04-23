using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ShopOnWheels.Domain.Models.Base;

namespace ShopOnWheels.Domain.Models.Product
{
    public class Product : BaseGuidEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public double Price { get; set; }
        public virtual ICollection<ProductList.ProductList> ProductList { get; set; } = new HashSet<ProductList.ProductList>();
    }
}
