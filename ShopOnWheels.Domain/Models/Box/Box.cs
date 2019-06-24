using ShopOnWheels.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopOnWheels.Domain.Models.Box
{
    public class Box : BaseGuidEntity
    {
        [Required]
        public int? Weight { get; set; }
        public ProductList.ProductList ProductList { get; set; }
    }
}
