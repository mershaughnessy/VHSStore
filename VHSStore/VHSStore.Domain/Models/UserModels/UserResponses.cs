using System;
using System.Collections.Generic;
using System.Text;

namespace VHSStore.Domain.Models.UserModels
{
    public class AddUserResponse : BaseResponse
    {
    }

    public class LoginUserResponse : BaseResponse
    {
        public string Body { get; set; }
    }

    public class GetAllUsersResponse : BaseResponse
    { 
        public IEnumerable<User> Body { get; set; }
    }

    public class DeleteUserResponse : BaseResponse
    { 
        
    }
}
