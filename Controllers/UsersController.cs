using CitasApp.Data;
using CitasApp.Entities;
using CitasApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CitasApp.Controllers;
[Authorize]
public class UsersController : BaseCont {
    private readonly IUserRepository _userRepository;
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() {
        return Ok(await _userRepository.GetUsersAsync());
    }
    [HttpGet("{username}")]
    public async Task<ActionResult<AppUser>> GetUser(string username){
        return Ok(await _userRepository.GetUserByUsernameAsync(username));
    }
}