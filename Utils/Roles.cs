using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SantafeApi.Infraestrucutre.Data;
using SantafeApi.Options;

namespace SantafeApi.Utils
{
    public class Roles : IRoles
    {
        public RoleManager<IdentityRole> RoleManager { get; }
        public UserManager<SantafeApiUser> UserManager { get; }
        private List<string> _roles = new List<string> { "ADMIN", "CUSTOMER" };

        public Roles(RoleManager<IdentityRole> roleManager, UserManager<SantafeApiUser> userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }
        public async Task CreateRolesIfNotExists()
        {
            foreach (string rol in _roles)
            {
                if (!await RoleManager.RoleExistsAsync(rol))
                {
                    await RoleManager.CreateAsync(new IdentityRole(rol));
                    Console.WriteLine($"Created Role {rol}");
                }
            }
        }
        public async Task CreateSuperUserIfNotExists(IConfiguration configuration)
        {

            var superUserEmail = configuration.GetSection("SuperUser:Email").ToString();
            var superUserPass = configuration.GetSection("SuperUser:Password").ToString();

            var roleExists = await RoleManager.RoleExistsAsync("ADMIN");
            var superUser = await UserManager.FindByEmailAsync(superUserEmail);

            if (superUser != null)
            {
                superUser = new SantafeApiUser()
                {
                    Email = superUserEmail,
                    UserName = "Santafe Admin"
                };
                var createPowerUser = await UserManager.CreateAsync(superUser, superUserPass);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(superUser, "Admin");
                }
            }
        }

        public async Task Setup(IConfiguration configuration)
        {
            await CreateRolesIfNotExists();
            await CreateSuperUserIfNotExists(configuration);
        }
    }
    public interface IRoles
    {
        Task CreateRolesIfNotExists();
        Task CreateSuperUserIfNotExists(IConfiguration configuration);
        Task Setup(IConfiguration configuration);
    }
}