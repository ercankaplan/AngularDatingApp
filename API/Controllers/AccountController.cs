
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;    
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;

        public AccountController(DataContext context,ITokenService tokenService,IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }
       
        
        
        // Other controller actions...

        
        [HttpPost("register")] 
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();

             var user = _mapper.Map<RegisterDto, AppUser>(registerDto);
             user.UserName = registerDto.Username.ToLower();
             user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
             user.PasswordSalt = hmac.Key;

            /*
            
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            
            */

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                 KnownAs = user.KnownAs
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.Include(i=> i.Photos).SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return BadRequest("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < hashedPassword.Length; i++)
            {
                if (hashedPassword[i] != user.PasswordHash[i]) return BadRequest("Invalid password");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                 KnownAs = user.KnownAs
            };
            
        }

        [HttpPost("register1")]  // without ApiController attribute we must do validation ourself like this
        public async Task<ActionResult<AppUser>> Register1([FromBody]RegisterDto registerDto)
        {

            if(!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errorMessages);
            }

             return new AppUser
                {
                    UserName = registerDto.Username
                };

        }


    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}

