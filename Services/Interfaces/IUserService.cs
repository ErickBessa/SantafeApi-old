using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SantafeApi.Infraestrucutre.Data;
using SantafeApi.Models;

namespace SantafeApi.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<SantafeApiUser> ManageUserAccessAsync(string codUser, int codCliente, bool hasAccess);
        Task<Dictionary<string, List<UserModel>>> ManageAllUserAccessAsync(List<UserAccessModel> usersModel);
    }
}