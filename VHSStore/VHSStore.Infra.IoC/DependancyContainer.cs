using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using VHSStore.Application.Interfaces;
using VHSStore.Infra.Data.Authentication;
using VHSStore.Infra.Data.Repositories;
using VHSStore.Schedules.Jobs;

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

            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());

            services.AddHangfireServer();
            services.AddSingleton<IEmailJob, EmailJob>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddSingleton<IJwtAuthenticationManager>(x => new JwtAuthenticationManager(key));
        }

        public static void ConfigureScheduleJobs(this IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            recurringJobManager.AddOrUpdate(
                "Email News Letter",
                () => serviceProvider.GetService<IEmailJob>().NewsLetterEmail(),
                "* * * * *");
        }

    }
}
