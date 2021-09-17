using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SantafeApi.Infraestrucutre.Data;
using Microsoft.Extensions.Hosting;

[assembly: HostingStartup(typeof(SantafeApi.Infraestrucutre.Identity.IdentityHostingStartup))]
namespace SantafeApi.Infraestrucutre.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {

        public void Configure(IWebHostBuilder builder)
        {

            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<SantafeApiContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SantafeApiLinuxConnection")));

                services.AddDefaultIdentity<SantafeApiUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<SantafeApiContext>();
            });

        }
    }
}