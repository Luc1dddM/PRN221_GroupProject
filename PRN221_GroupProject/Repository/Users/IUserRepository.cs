using Microsoft.AspNetCore.Identity;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Users
{
    public interface IUserRepository
    {
        Task<PagedResultDTO<UserListDTO>> GetUsers(string[] statusesParam, string sortBy, string sortOrder, string[] rolesParam, string searchTerm, int pageNumber, int pageSize);
        Task<List<ApplicationUser>> GetUsersAsync();
        Task<ApplicationUser> FindUserByIdAsync(string id);
        Task<IdentityResult> DeleteUser(ApplicationUser user);
        Task<IdentityResult> CreateUser(Create.InputModel input);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
        Task<IdentityResult> EditUser(string id, EditModel.InputModel input);
        Task<string> GetUserNameById(string id);
        public Task ImportUsers(IFormFile excelFile);
        public Task<Byte[]> ExportUsers(string[] statusesParam, string searchterm, int pageNumberParam, int pageSizeParam);
    }
}
