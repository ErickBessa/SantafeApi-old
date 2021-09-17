using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SantafeApi.Options;
using SantafeApi.Services;
using SantafeApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Installers
{
    public class FacebookAuthInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var facebookSettings = configuration.GetSection("FacebookAuthSettings");
            services.Configure<FacebookAuthSettings>(facebookSettings);
            services.AddSingleton(facebookSettings);

            services.AddHttpClient().AddCors();
            services.AddSingleton<IFacebookAuthService, FacebookAuthService>();
        }
    }
}
