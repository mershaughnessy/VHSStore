using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHSStore.Utility.Middleware.ExceptionMiddlewares
{
    public static class ConfigureExceptionMiddleware
    {
        public static void CustomConfigurationExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
