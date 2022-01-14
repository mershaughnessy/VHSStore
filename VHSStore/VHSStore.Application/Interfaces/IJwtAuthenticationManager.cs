using System;
using System.Collections.Generic;
using System.Text;

namespace VHSStore.Application.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate();
    }
}
