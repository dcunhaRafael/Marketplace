using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using InsuranceApi.Infra.CrossCutting.Identity;
using InsuranceApi.Infra.CrossCutting.Identity.Extensions;
using InsuranceApi.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Web.Controllers {

    [Route("api/autenticacao")]
    public class AuthenticationController : BaseController {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppSettings appSettings;

        public AuthenticationController(IOptions<AppSettings> appSettings, IUser user, ILogger<AuthenticationController> logger,
                                        SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                        IAuditApplication auditApplication) : base(user, logger, auditApplication) {
            this.appSettings = appSettings.Value;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost("token")]
        public async Task<ActionResult> LoginAsync(LoginUsuarioViewModel loginViewModel) {
            var request = new { loginViewModel.Login, Password = string.IsNullOrWhiteSpace(loginViewModel.Password) ? "não informada" : new string('*', loginViewModel.Password.Length) };
            try {

                base.WriteAuditData(LogLevel.Trace, "Autenticação", request, null);

                if (!ModelState.IsValid) return base.ReturnErrorAndLogModelState(ModelState, loginViewModel);

                var result = await signInManager.PasswordSignInAsync(loginViewModel.Login, loginViewModel.Password, false, true);
                if (result.IsLockedOut) {
                    throw new Exception("Usuário temporariamente bloqueado por tentativas inválidas.");
                }
                if (result.Succeeded) {
                    var jwt = await GerarJwt(loginViewModel.Login);
                    base.WriteAuditData(LogLevel.Debug, "Autenticação", request, jwt);
                    return base.ReturnSuccess(jwt);
                } 

                throw new Exception("Usuário ou senha incorretos");

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Autenticação", request, e);
                return ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }

        private async Task<LoginRetornoViewModel> GerarJwt(string login) {
            var user = await userManager.FindByNameAsync(login);
            var claims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim("userName", user.UserName));
            claims.Add(new Claim("extID", user.ExternalId.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles) {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor {
                Issuer = appSettings.Emissor,
                Audience = appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginRetornoViewModel {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(appSettings.ExpiracaoHoras).TotalSeconds,
                Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}