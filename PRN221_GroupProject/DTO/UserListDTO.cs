using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.DTO
{
    public class UserListDTO
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class PagedResultDTO<GetUser>
    {
        public List<GetUser> Users { get; set; }
        public int totalPages { get; set; }
    }
}
