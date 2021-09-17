using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SantafeApi.Infraestrucutre.Data;
using SantafeApi.Models;
using SantafeApi.Services.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantafeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private SantafeApiContext _dbContext;
        private readonly UserManager<SantafeApiUser> _userManager;
        private readonly SignInManager<SantafeApiUser> _signInManager;
        private readonly IMailApiService _mailService;
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly IIdentityService _identityService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(
            IConfiguration configuration, SantafeApiContext dbContext,
            UserManager<SantafeApiUser> userManager, SignInManager<SantafeApiUser> signInManager,
            IMailApiService mailService, IFacebookAuthService facebookAuthService,
            IIdentityService identityService, RoleManager<IdentityRole> roleManager
            )
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _facebookAuthService = facebookAuthService;
            _identityService = identityService;
            _roleManager = roleManager;
        }
        /// <summary>
        ///		Efetua o login no sistema e gerar um token do tipo - Bearer JWT
        /// </summary>
        /// <param name="loginModel">loginModel</param>
        /// <returns>Retorna um token token JWT</returns>
        /// <response code="200">Retorna o JWT</response>
        /// <response code="400">Senha passada é inválida</response>
        /// <response code="404">Usuário não existe</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == loginModel.Email);
            if (user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

                if (signInResult.Succeeded)
                {
                    var tokenString = _identityService.GenerateJwtToken(user);
                    return Ok(new { Token = tokenString });
                }
                else
                {
                    return BadRequest(new ErrorModel("Senha e/ou e-mail inválidos") { FieldName = "Password" });
                }

            }
            return NotFound(new ErrorModel("Usuário não existe"));
        }

        /// <summary>
        ///		Efetua o login no sistema usando acesso via facebook e gera um token do tipo - Bearer JWT
        /// </summary>
        /// <param name="accessToken">accessToken</param>
        /// <returns>Retorna um token token JWT</returns>
        /// <response code="200">Retorna o JWT</response>
        /// <response code="400">Erro durante a criação do usuário</response>
        [AllowAnonymous]
        [HttpPost("login-with-facebook")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LoginWithFacebook(string accessToken)
        {
            var validatedTokenResult = await _facebookAuthService.ValidateAccessTokenAsync(accessToken);

            if (!validatedTokenResult.Data.IsValid)
            {
                return BadRequest(new ErrorModel()
                {
                    Message = "Invalid Facebook Token"
                });
            }

            var userInfo = await _facebookAuthService.GetUserAsync(accessToken);

            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var identityUser = new SantafeApiUser
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    HasAccess = false
                };

                if (!await _roleManager.RoleExistsAsync("CUSTOMER"))
                    await _roleManager.CreateAsync(new IdentityRole("CUSTOMER"));

                await _userManager.AddToRoleAsync(identityUser, "CUSTOMER");

                var createdResult = await _userManager.CreateAsync(identityUser);
                if (!createdResult.Succeeded)
                {
                    var stringBuilder = new StringBuilder();
                    foreach (var error in createdResult.Errors)
                    {
                        stringBuilder.Append(error.Description);
                        stringBuilder.Append("\r\n");
                    }
                    return BadRequest(new ErrorModel(stringBuilder.ToString()));
                }

                var token = _identityService.GenerateJwtToken(identityUser);
                return Ok(new { Token = token });
            }
            var tokenString = _identityService.GenerateJwtToken(user);
            return Ok(new { Token = tokenString });
        }
        /// <summary>
        ///		Efetua o registro de um novo usuário.
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns>Retorna uma mensagem de usuário cadastrado.</returns>
        /// <response code="200"> retorna uma mensagem de sucesso.</response>
        /// <response code="400"> retorna os possíveis erros do esquema de Model,</response>
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var persistedUser = await _userManager.FindByEmailAsync(registerModel.Email);
            if (persistedUser != null)
                BadRequest(new ErrorModel("Email já cadastrado"));

            SantafeApiUser santafeApiUser = new()
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
                HasAccess = false
            };

            var result = await _userManager.CreateAsync(santafeApiUser, registerModel.Password);

            if (result.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync("CUSTOMER"))
                    await _userManager.AddToRoleAsync(santafeApiUser, "CUSTOMER");
                return Ok(new { Message = "Usuário criado com sucesso" });
            }
            else
            {
                var stringBuilder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    stringBuilder.Append(error.Description);
                    stringBuilder.Append("\r\n");
                }
                return BadRequest(new ErrorModel(stringBuilder.ToString()));

            };
        }

        /// <summary>
        /// Solicita a geração de um token através do Email do usuário registrado.
        /// </summary>
        /// <param name="resetPasswordModel">Usado para receber o Email do usuário</param>
        /// <returns>Retorna um token válido usado para resetar a senha do usuário.</returns>
        /// <response code="200"> Retorna um JWT</response>
        /// <response code="400"> Retorna mensagem de erro de usuário não encontrado</response>
        /// <response code="500"> Serviço de email fora do ar</response>
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                return NotFound(new ErrorModel
                {
                    Message = "O usuário não existe",
                    FieldName = "Email"
                });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            try
            {
                _mailService.SendEmail(user.Email, token);
                return Ok(new { Message = "Email enviado com sucesso!" });
            }
            catch (Exception e)
            {
                return Problem(title: e.Message, detail: e.StackTrace);
            }
        }
        /// <summary>
        ///		Valida token passado e reseta a senha se for válido.
        /// </summary>
        /// <param name="resetPasswordTokenModel"></param>
        /// <returns> Mensagem de sucesso</returns>
        /// <response code="200">Mesagem de sucesso</response>
        /// <response code="400">Erros gerados pelo UserManager</response>
        /// <response code="404">Quando usuário não é encontrado</response>
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut("reset-password-with-token")]
        public async Task<IActionResult> ResetPasswordWithToken([FromBody] ResetPasswordTokenModel resetPasswordTokenModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordTokenModel.Email);

            if (user == null)
                return NotFound(new ErrorModel("user not found"));

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordTokenModel.Token, resetPasswordTokenModel.Password);
            if (result.Succeeded)
                return Ok(new { Message = "Senha alterada com sucesso" });

            var stringBuilder = new StringBuilder();
            foreach (var error in result.Errors)
            {
                stringBuilder.Append(error.Description);
                stringBuilder.Append("\r\n");
            }
            return BadRequest(new ErrorModel(stringBuilder.ToString()));
        }
    }
}

