

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SantafeApi.Services;
using SantafeApi.Services.Interfaces;

namespace SantafeApi.Installers
{
    public class UserServiceIsntaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
