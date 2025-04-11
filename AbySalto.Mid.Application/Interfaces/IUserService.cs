using System;
using AbySalto.Mid.Application.DTOs;

namespace AbySalto.Mid.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> RegisterAsync(RegisterUserDto registerDto);
    Task<string> LoginAsync(LoginUserDto loginDto); 
    Task<UserDto> GetCurrentUserAsync();
}
