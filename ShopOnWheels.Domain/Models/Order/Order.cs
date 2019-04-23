using ShopOnWheels.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopOnWheels.Domain.Models.Order
{
    public class Order : BaseGuidEntity
    {

        [Required]
        [ForeignKey("UserId")]
        public virtual User.User User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime OrderDeliver { get; set; }

        public int? Frequency { get; set; }

        public double? Total { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<ProductList.ProductList> ProductList { get; set; } = new HashSet<ProductList.ProductList>();
    }
}
