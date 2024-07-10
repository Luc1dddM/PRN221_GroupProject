using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Messages;

namespace PRN221_GroupProject.Repository.Message
{
    public class MessageRepository : IMessageRepository
    {
        private readonly Prn221GroupProjectContext _context;
        public MessageRepository(Prn221GroupProjectContext context)
        {
            _context = context;
        }

        public void CreateMessage(string senderId, string groupName, Models.Message message)
        {
            try
            {
                message.SenderId = senderId;
                message.GroupName = groupName;
                message.SendDate = DateTime.Now;
                _context.Messages.Add(message);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Models.Message> GetAllMessage(string groupName)
        {
            try
            {
                return _context.Messages.Include(m=>m.UserMessages).Where(m => m.GroupName.Equals(groupName)).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
