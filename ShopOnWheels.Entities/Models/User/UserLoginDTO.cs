﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ShopOnWheels.Entities.Models.Base;

namespace ShopOnWheels.Entities.Models.User
{
    public class UserLoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
