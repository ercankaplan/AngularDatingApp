
using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class BuggyController : BaseApiController
{

    private readonly DataContext _context;
    public BuggyController(DataContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet("auth")]
    public async Task<IActionResult> GetSecret()
    {
        return Ok("Secret text");
    }
    [HttpGet("not-found")]
    public async Task<IActionResult> GetNotFound()
    {
       var notFoundUser = await _context.Users.FindAsync(-1);
       if (notFoundUser == null) return NotFound();
       else return Ok(notFoundUser);
    }
    [HttpGet("server-error")]
    public async Task<IActionResult> GetServerError()
    {
        var notFoundUser = await _context.Users.FindAsync(-1);
        var result = notFoundUser.ToString();
        return Ok(result);
    }
    
    [HttpGet("bad-request")]
    public async Task<IActionResult> GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }




}