using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        public bool IsCountable { get; set; }    
        [Required]
        [ForeignKey("CategoryId")]
        public virtual Category.Category Category { get; set; }
        public virtual ICollection<ProductList.ProductList> ProductList { get; set; } = new HashSet<ProductList.ProductList>();
    }
}
