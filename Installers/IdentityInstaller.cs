using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SantafeApi.Services;
using SantafeApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Installers
{
    public class IdentityInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
