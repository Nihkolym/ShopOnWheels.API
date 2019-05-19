using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;
using ShopOnWheels.Entities.Models.Base;

namespace ShopOnWheels.Entities.Models.Product
{
    public class ProductDTO : BaseGuidDTOEntity
    {
        [Required]
        public string Name { get; set; }
        public int Weight { get; set; }
        public string Image { get; set; }
        [Required]
        public bool IsCountable { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
