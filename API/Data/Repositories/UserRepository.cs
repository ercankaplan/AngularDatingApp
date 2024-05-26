using API.Data;
using API.DTOs;
using API.Entities;
using API.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{

    private readonly DataContext _context;
    private IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Update(AppUser user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await _context.Users
            .Include(p => p.Photos)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            /*.Select(u => new MemberDto
            {
                Id = u.Id,
                Age = u.DateOfBirth.CalculateAge(),
                City = u.City,
                Country = u.Country,
                Created = u.Created,
                KnownAs = u.KnownAs,
                LastActive = u.LastActive,
                UserName = u.UserName,
                Gender = u.Gender,
                Interests = u.Interests,
                Introduction = u.Introduction,
                LookingFor = u.LookingFor,
                PhotoUrl = u.Photos.FirstOrDefault(p => p.IsMain).Url,
                Photos = u.Photos.Select(p => new PhotoDto
                {
                    Id = p.Id,
                    Url = p.Url
                }).ToList()
            })*/
            .ToListAsync();
    }

    public async Task<MemberDto> GetMemberByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(p => p.Photos)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            /*.Select(u => new MemberDto
            {
                Id = u.Id,
                Age = u.DateOfBirth.CalculateAge(),
                City = u.City,
                Country = u.Country,
                Created = u.Created,
                KnownAs = u.KnownAs,
                LastActive = u.LastActive,
                UserName = u.UserName,
                Gender = u.Gender,
                Interests = u.Interests,
                Introduction = u.Introduction,
                LookingFor = u.LookingFor,
                PhotoUrl = u.Photos.FirstOrDefault(p => p.IsMain).Url,
                Photos = u.Photos.Select(p => new PhotoDto
                {
                    Id = p.Id,
                    Url = p.Url
                }).ToList()
            })*/
            .FirstOrDefaultAsync(u => u.UserName == username);
    }
}