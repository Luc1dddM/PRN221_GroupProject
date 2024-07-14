using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Messages
{
    public interface IMessageRepository
    {
        public List<PRN221_GroupProject.Models.Message> GetAllMessage(string groupName);
        public void CreateMessage(string senderId, string groupName, PRN221_GroupProject.Models.Message message);
    }
}
