using MovieApp.Business.DTOs.UserDTOs;

namespace MovieApp.Business.Services.Interfaces;

public interface IAuthService
{
    Task Register(UserRegiterDto dto);
    Task Login(UserLoginDto dto);
}
