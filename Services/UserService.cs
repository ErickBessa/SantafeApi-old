using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SantafeApi.Infraestrucutre.Data;
using SantafeApi.Models;
using SantafeApi.Services.Interfaces;

namespace SantafeApi.Services
{
    public class UserService : IUserService
    {
        public UserManager<SantafeApiUser> UserManager { get; }
        public UserService(UserManager<SantafeApiUser> userManager)
        {
            UserManager = userManager;
        }
        public async Task<SantafeApiUser> ManageUserAccessAsync(string codUser, int codCliente, bool hasAccess)
        {
            var user = UserManager.FindByIdAsync(codUser).Result;
            user.HasAccess = hasAccess;
            user.CodCliente = codCliente;

            await UserManager.UpdateAsync(user);
            return user;
        }
        public async Task<Dictionary<string, List<UserModel>>> ManageAllUserAccessAsync(List<UserAccessModel> usersModel)
        {
            var usersStatusAccess = new Dictionary<string, List<UserModel>>();
            usersStatusAccess.Add("Liberado", new List<UserModel>());
            usersStatusAccess.Add("Revogado", new List<UserModel>());

            foreach (var userModel in usersModel)
            {
                var persistedUser = await UserManager.FindByIdAsync(userModel.UserId);
                if (persistedUser != null)
                {
                    persistedUser.HasAccess = userModel.HasAccess;
                    persistedUser.CodCliente = userModel.CodCliente;
                }
                await UserManager.UpdateAsync(persistedUser);

                if (persistedUser.HasAccess)
                {
                    usersStatusAccess["Liberado"].Add(
                        new UserModel()
                        {
                            CodCliente = persistedUser.CodCliente.Value,
                            UserId = persistedUser.Id,
                            HasAccess = persistedUser.HasAccess,
                            UserEmail = persistedUser.Email,
                        });

                }
                else
                {
                    usersStatusAccess["Revogado"].Add(
                        new UserModel()
                        {
                            CodCliente = persistedUser.CodCliente.Value,
                            UserId = persistedUser.Id,
                            HasAccess = persistedUser.HasAccess,
                            UserEmail = persistedUser.Email,
                        });
                }
            }

            return usersStatusAccess;
        }

        public void Dispose()
        {
            UserManager.Dispose();
        }
    }
}