using ShopOnWheels.Entities.Models.Base;
using ShopOnWheels.Entities.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopOnWheels.Entities.Models.Category
{
    public class CategoryDTO : BaseGuidDTOEntity
    {
        [Required]
        public string Name;
        public virtual List<ProductDTO> Products { get; set; }
    }
}
