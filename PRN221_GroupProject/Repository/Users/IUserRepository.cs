using Microsoft.AspNetCore.Identity;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Models.DTO;

namespace PRN221_GroupProject.Repository.Users
{
    public interface IUserRepository
    {
        Task<PagedResultDTO<UserListDTO>> GetUsersAsync(string[] statusesParam, string[] rolesParam, string searchTerm, int pageNumber, int pageSize);
        Task<ApplicationUser> FindUserByIdAsync(string id);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);

        Task<IdentityResult> CreateUserAsync(Create.InputModel input);
        Task<ApplicationUser> FindUserByEmailAsync(string email);

       Task<IdentityResult> EditUserAsync(string id, EditModel.InputModel input);
       Task<string> GetUserRoleAsync(ApplicationUser user); 
    }
}
