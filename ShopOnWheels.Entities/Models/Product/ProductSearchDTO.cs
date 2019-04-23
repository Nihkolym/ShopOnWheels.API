using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnWheels.Entities.Models.Product
{
    public class ProductSearchDTO
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public double? From { get; set; }
        public double? To { get; set; }
    }
}
