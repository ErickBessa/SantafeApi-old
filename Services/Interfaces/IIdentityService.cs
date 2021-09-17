using SantafeApi.Infraestrucutre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GenerateJwtToken(SantafeApiUser user);

    }
}
