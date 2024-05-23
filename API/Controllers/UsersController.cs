using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[Authorize]
public class UsersController(DataContext context) : BaseApiController
{
    private readonly DataContext _context = context;

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() //api/users
    {
        return await _context.Users.ToListAsync();
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id) //api/users/3
    {
        return await _context.Users.FindAsync(id);
    }
}


