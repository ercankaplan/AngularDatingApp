
using API.DTOs;
using API.Entities;

namespace API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);

        void Update(AppUser user);

        Task<bool> SaveAllAsync();

        Task<AppUser> GetUserByUsernameAsync(string username);

         Task<IEnumerable<MemberDto>> GetMembersAsync();
          Task<MemberDto> GetMemberByUsernameAsync(string username);
        
    }
}
