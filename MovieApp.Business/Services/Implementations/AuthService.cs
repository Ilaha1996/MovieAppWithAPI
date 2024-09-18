using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Business.DTOs.TokenDTOs;
using MovieApp.Business.DTOs.UserDTOs;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApp.Business.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }
    public async Task<TokenResponseDto> Login(UserLoginDto dto)
    {
        AppUser appUser = null;
        appUser = await _userManager.FindByNameAsync(dto.Username);

        if (appUser == null)
        {
            throw new NullReferenceException("Invalid Credentials");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, dto.RememberMe);
        if(!result.Succeeded)
        {
            throw new NullReferenceException("Invalid Credentials");
        }

        var roles = await _userManager.GetRolesAsync(appUser);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier,appUser.Id),
            new Claim(ClaimTypes.Name,appUser.UserName),
            new Claim("Fullname",appUser.Fullname),
        };
        
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role,role)));
        string secretKey = _configuration.GetSection("JWT:secretKey").Value;
        DateTime expiredDate = DateTime.UtcNow.AddMinutes(2);


        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            signingCredentials : signingCredentials,
            claims : claims,
            audience: _configuration.GetSection("JWT:audience").Value,
            issuer: _configuration.GetSection("JWT:issuer").Value,
            expires: expiredDate,
            notBefore: DateTime.UtcNow);

        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return new TokenResponseDto(token,expiredDate);
    }

    public async Task Register(UserRegiterDto dto)
    {
        AppUser appUser = new AppUser()
        {
            Email = dto.Email,
            Fullname = dto.Fullname,
            UserName = dto.Username

        };
        var result = await _userManager.CreateAsync(appUser, dto.Password);

        if (!result.Succeeded)
        {
            // TODO : Exception create
            throw new NullReferenceException();
        }
    }
}
