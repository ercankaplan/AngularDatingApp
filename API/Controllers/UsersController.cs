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
public class UsersController(IUserRepository userRepository, IPhotoService photoService, IMapper mapper) : BaseApiController
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPhotoService _photoService = photoService;
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


    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUserByUserName(string username) //api/users/ecco
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

        if (user == null)
            return NotFound();

        _mapper.Map(memberUpdateDto, user);

        _userRepository.Update(user);

        if (await _userRepository.SaveAllAsync())
            return NoContent();//200 empty response

        return BadRequest("Failed to update user");

    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> UploadPhoto(IFormFile file)
    {



        var userName = User.GetUsername();  //get extention method  User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userRepository.GetUserByUsernameAsync(userName);

        if (user == null)
            return NotFound("User not found");

        var ImageUploadResult = await _photoService.AddPhotoAsync(file);

        if (ImageUploadResult.Error != null)
            return BadRequest(ImageUploadResult.Error.Message);

        var photo = new Photo
        {
            Url = ImageUploadResult.SecureUrl.AbsoluteUri,
            PublicId = ImageUploadResult.PublicId,
            IsMain = user.Photos.Count == 0
        };

        user.Photos.Add(photo);

        if (!await _userRepository.SaveAllAsync())
            return BadRequest("Problem adding photo");


        //return Ok(_mapper.Map<PhotoDto>(photo));
        return CreatedAtAction(nameof(GetUserByUserName), new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
    }

    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {

        var userName = User.GetUsername();
        var user = await _userRepository.GetUserByUsernameAsync(userName);

        if (user == null)
            return NotFound("User not found");

        var mainPhoto = user.Photos.Where(x => x.IsMain).FirstOrDefault();
        if (mainPhoto != null)
            mainPhoto.IsMain = false;

        var photo = user.Photos.Where(x => x.Id == photoId).FirstOrDefault();
        if (photo == null)
            return NotFound("Photo not found");

        photo.IsMain = true;

        return await _userRepository.SaveAllAsync() ? NoContent() : BadRequest("Failed to set main photo");

    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {

        var userName = User.GetUsername();
        var user = await _userRepository.GetUserByUsernameAsync(userName);

        if (user == null)
            return NotFound("User not found");

        var photo = user.Photos.Where(x => x.Id == photoId).FirstOrDefault();
        if (photo == null)
            return NotFound("Photo not found");

        if (photo.IsMain)
            return BadRequest("You cannot delete your main photo");

        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null)
                return BadRequest(result.Error.Message);
        }

        user.Photos.Remove(photo);

        return await _userRepository.SaveAllAsync() ? NoContent() : BadRequest("Failed to delete the photo");

    }

}


