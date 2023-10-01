using CitasApp.Data;
using CitasApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CitasApp.Controllers;
[Authorize]
public class UsersController : BaseCont {
    private readonly DataContext _context;
    public UsersController(DataContext context)
    {
        _context = context;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() {
        return await _context.Users.ToListAsync();
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id){
        return await _context.Users.FindAsync(id);
    }
}