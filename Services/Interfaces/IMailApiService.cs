using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Services.Interfaces
{
    public interface IMailApiService
    {
        void SendEmail(string to, string token);
    }
}
