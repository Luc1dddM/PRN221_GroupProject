using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Groups
{
    public class GroupRepository : IGroupRepository
    {
        private readonly Prn221GroupProjectContext _context;

        public GroupRepository(Prn221GroupProjectContext context)
        {
            _context = context;
        }

        public List<Group> GetGroups()
        {
            try
            {
                return _context.Groups.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
