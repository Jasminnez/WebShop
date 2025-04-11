using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AbySalto.Mid.Application.DTOs;
using AbySalto.Mid.Application.Interfaces;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Infrastructure.Persistence;
using AbySalto.Mid.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.Application.Services;

public class UserService(AppDbContext context, IJwtService tokenService, IHttpContextAccessor httpContextAccessor) : IUserService
{
    public async Task<UserDto> GetCurrentUserAsync()
    {
        var username = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(username))
            throw new Exception("User not found");

        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
            throw new Exception("User not found");

        return new UserDto
        {
            Username = user.Username,
            Email = user.Email,
            Token = tokenService.CreateToken(user)
        };
    }

     public async Task<string> LoginAsync(LoginUserDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == dto.Username.ToLower());

        if (user == null) return string.Empty;

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                return string.Empty;
        }

        return tokenService.CreateToken(user);
    }

    public async Task<UserDto> RegisterAsync(RegisterUserDto dto)
    {
        if (await context.Users.AnyAsync(x => x.Email.ToLower() == dto.Email.ToLower()))
            throw new Exception("Email already exists");

        using var hmac = new HMACSHA512();

        var user = new User
        {
            Username = dto.Username.ToLower(),
            Email = dto.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserDto
        {
            Username = user.Username,
            Email = user.Email,
            Token = tokenService.CreateToken(user)
        };
    }
}
