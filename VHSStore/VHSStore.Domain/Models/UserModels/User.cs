﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VHSStore.Domain.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
    }
}