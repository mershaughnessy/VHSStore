using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace VHSStore.Domain.Models
{
    public class BaseResponse<T>
    {
        public T Body { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
