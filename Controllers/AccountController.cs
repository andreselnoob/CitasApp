using CitasApp.Data;
using CitasApp.DTOs;
using CitasApp.Entities;
using CitasApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace CitasApp.Controllers;

public class AccountController:BaseCont
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    private const string User_pass_mess = "Usuario o Password incorrecto";
    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService= tokenService;
    }

    [HttpPost("register")]
    public async Task <ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username))
        return BadRequest("Ya existe ese nombre de usuario");

        using var hmac=new HMACSHA512();
        var user = new AppUser
        {
            UserName = registerDto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new UserDto
        {
            Username = user.UserName,
            token = _tokenService.CreateToken(user)
        };

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x =>
        x.UserName.ToLower()==loginDto.Username.ToLower());
        if (user == null) return Unauthorized(User_pass_mess);
        using var hmac=new HMACSHA512(user.PasswordSalt);
        var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for(int i = 0;i<computedhash.Length; i++)
        {
            if (computedhash[i] != user.PasswordHash[i]) return Unauthorized(User_pass_mess);
        }

        return new UserDto
        {
            Username = user.UserName,
            token = _tokenService.CreateToken(user)
        };
    }
    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower()); 
    }

}
