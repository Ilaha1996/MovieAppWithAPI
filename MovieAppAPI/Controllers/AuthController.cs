using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.DTOs.UserDTOs;
using MovieApp.Business.Services.Interfaces;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
                 _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult>Register(UserRegiterDto dto)
        {
            try
            {
                await _authService.Register(dto);
            }
            catch(NullReferenceException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();  
            }
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            try
            {
                await _authService.Login(dto);
            }
            catch (NullReferenceException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("SuperAdmin");
        //    IdentityRole role2 = new IdentityRole("Admin");
        //    IdentityRole role3 = new IdentityRole("Member");
        //    IdentityRole role4 = new IdentityRole("Editor");

        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role4);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);

        //    return Ok();
        //}

        //[HttpGet]
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser appUser = new AppUser()
        //    {
        //        UserName = "Ilaha",
        //        Email = "ilahahasanova@yahoo.com",
        //        Fullname = "Ilaha Hasanova"
        //    };

        //    await _userManager.CreateAsync(appUser, "Salam123!");
        //    return Ok();

        //}

        //[HttpGet]
        //public async Task<IActionResult> AddRole()
        //{
        //    AppUser appUser = await _userManager.FindByNameAsync("Ilaha");

        //    await _userManager.AddToRoleAsync(appUser, "SuperAdmin");
        //    return Ok();

        //}

    }
}
