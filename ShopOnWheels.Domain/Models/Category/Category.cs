﻿using ShopOnWheels.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopOnWheels.Domain.Models.Category
{
    public class Category : BaseGuidEntity
    {
        [Required]
        public string Name { set; get; }    
        public virtual ICollection<Product.Product> Products { get; set; } = new List<Product.Product>();
    }
}
