using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using VHSStore.Application.Interfaces;
using VHSStore.Infra.Data.Authentication;
using VHSStore.Infra.Data.Repositories;

namespace VHSStore.Infra.IoC
{
    public static class DependancyContainer
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            var key = "b48a4612-ec04-46f0-9ecf-d267cfe44813";
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IJwtAuthenticationManager>(x => new JwtAuthenticationManager(key));
        }
    }
}
