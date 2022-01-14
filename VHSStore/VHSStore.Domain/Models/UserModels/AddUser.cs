using System;
using System.Collections.Generic;
using System.Text;

namespace VHSStore.Domain.Models.UserModels
{
    public class AddUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
