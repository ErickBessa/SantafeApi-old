using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SantafeApi.Entities;

namespace SantafeApi.Infraestrucutre.Data
{
    // Add profile data for application users by adding properties to the SantafeApiUser class
    public class SantafeApiUser : IdentityUser
    {

        public bool HasAccess { get; set; }

        public int? CodCliente { get; set; }

        public virtual Cliente ClienteNavigation { get; set; }
    }

}

