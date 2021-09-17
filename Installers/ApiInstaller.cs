using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SantafeApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Installers
{
    public class ApiInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services
                .AddControllers(options => options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

        }
    }
}
