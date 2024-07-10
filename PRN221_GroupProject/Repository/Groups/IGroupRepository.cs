using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Groups
{
    public interface IGroupRepository
    {
        public List<PRN221_GroupProject.Models.Group> GetGroups();
    }
}
