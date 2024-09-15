using MovieApp.Business.DTOs.TokenDTOs;
using MovieApp.Business.DTOs.UserDTOs;

namespace MovieApp.Business.Services.Interfaces;

public interface IAuthService
{
    Task Register(UserRegiterDto dto);
    Task<TokenResponseDto> Login(UserLoginDto dto);
}
