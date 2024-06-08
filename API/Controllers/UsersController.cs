using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    //[AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() //api/users
    {
        
       
        var users = await _userRepository.GetUsersAsync();

        if (!users.Any())
            return NotFound();

        var members = _mapper.Map<IEnumerable<MemberDto>>(users);

        return Ok(members);
    }

     [HttpGet]
     [Route("/api/members")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> Members() //api/users
    {
        
        var members = await _userRepository.GetMembersAsync();

        if (!members.Any())
            return NotFound();

        return Ok(members);

        /*
        var users = await _userRepository.GetUsersAsync();

        if (!users.Any())
            return NotFound();

        var members = _mapper.Map<IEnumerable<MemberDto>>(users);

        return Ok(members);
        */
    }



    [HttpGet("/api/members/{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username) //api/users/ecco
    {

        var member = await _userRepository.GetMemberByUsernameAsync(username);

        if (member == null)
            return NotFound();

        return Ok(member);
        /*
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null)
            return NotFound();

        var member = _mapper.Map<MemberDto>(user);

        return Ok(member);
        */
    }
    [HttpGet("/{id}")]//https://localhost:5001/1
    public async Task<ActionResult<AppUser>> GetUserById(int id) //api/users/3
    {

        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
            return NotFound();

        var member = _mapper.Map<MemberDto>(user);

        return Ok(member);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        //var userName = this.HttpContext.User.Identity.Name;
        var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userRepository.GetUserByUsernameAsync(userName);

        if(user == null)
            return NotFound();

        _mapper.Map(memberUpdateDto, user);
        
        _userRepository.Update(user);

        if (await _userRepository.SaveAllAsync())
            return NoContent();//200 empty response

        return BadRequest("Failed to update user");
       
    }

}


