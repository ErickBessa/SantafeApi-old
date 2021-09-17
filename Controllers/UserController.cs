using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SantafeApi.Infraestrucutre.Data;
using SantafeApi.Models;
using SantafeApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {

        public IUserService UserService;

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPut("manage-access")]
        public async Task<IActionResult> ManageAccess(UserAccessModel model)
        {
            var user = await UserService.ManageUserAccessAsync(model.UserId, model.CodCliente, model.HasAccess);
            if (user != null)
            {
                if (user.HasAccess)
                    return Ok($"Acesso do usuário: {user.UserName} liberado.");

                return Ok($"Acesso do usuário: {user.UserName} revogado.");
            }
            return BadRequest(new ErrorModel { Message = "Usuário não encontrado." });
        }
        [HttpPut("manage-all-access")]
        public async Task<IActionResult> ManageAllAccess(List<UserAccessModel> model)
        {
            var user = await UserService.ManageAllUserAccessAsync(model);
            return Ok(user);
        }
    }
}
