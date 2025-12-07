using Api_InfoTransito.DTOs;
using Api_InfoTransito.Extensions;
using Business_InfoTransito.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_InfoTransito.Controllers;

[Route("api")]
public class AuthController : MainController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings;

    public AuthController(INotifier notifier,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IOptions<AppSettings> appSettings) : base(notifier)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }

    [HttpPost("nova-conta")]
    public async Task<ActionResult> Register(RegisterUserDto registerUser)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var user = new IdentityUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if (result.Succeeded) 
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return CustomResponse(await GenerateJwt(user.Email));
        }
        foreach (var error in result.Errors)
        {
            NotifyError(error.Description);
        }

        return CustomResponse(registerUser);
    }

    [HttpPost("entrar")]
    public async Task<ActionResult> Login(LoginUserDto loginUser)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _signInManager.PasswordSignInAsync(
            loginUser.Email, loginUser.Password, isPersistent: false, lockoutOnFailure: true);

        if (result.Succeeded)
            return CustomResponse(await GenerateJwt(loginUser.Email));

        if (result.IsLockedOut)
        {
            NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
            return CustomResponse(loginUser);
        }

        NotifyError("Usuário ou Senha incorretos");
        return CustomResponse(loginUser);
    }

    private async Task<LoginResponseDto> GenerateJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
        foreach (var useRole in userRoles)
        {
            claims.Add(new Claim("role", useRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Sender,
            Audience = _appSettings.ValidatedOn,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encondedToken = tokenHandler.WriteToken(token);

        var response = new LoginResponseDto
        {
            AccessToken = encondedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
            UserToken = new UserTokenDto
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value})
            }
        };

        return response;
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}
