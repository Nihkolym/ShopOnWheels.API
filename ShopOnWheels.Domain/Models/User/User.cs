using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ShopOnWheels.Domain.Models.User
{
    public class User : IdentityUser
    {
        [MaxLength(36)]
        public override string Id { get; set; }
        [MaxLength(36)]
        public override string UserName { get; set; }
        [MaxLength(36)]
        public string FirstName { get; set; }
        [MaxLength(36)]
        public string LastName { get; set; }
        [MaxLength(36)]
        public string Address { get; set; }
        public ICollection<Order.Order> Orders { get; set; } = new List<Order.Order>();
    }
}
