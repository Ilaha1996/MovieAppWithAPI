using Microsoft.AspNetCore.Identity;
using MovieApp.Business.DTOs.UserDTOs;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Core.Entities;

namespace MovieApp.Business.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task Login(UserLoginDto dto)
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
