using System;
using System.Collections.Generic;
using System.Text;

namespace VHSStore.Domain.Models
{
    public class BaseResponse
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
