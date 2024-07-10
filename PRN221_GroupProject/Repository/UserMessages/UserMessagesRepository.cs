using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Users;

namespace PRN221_GroupProject.Repository.UserMessages
{
    public class UserMessagesRepository : IUserMessagesRepository
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly IUserRepository _userMessagesRepository;
        public UserMessagesRepository(Prn221GroupProjectContext context, IUserRepository userMessagesRepository)
        {
            _context = context;
            _userMessagesRepository = userMessagesRepository;
        }

        public void CreateAdminUserMessageAsync(string messageId, string groupName)
        {
            try
            {
                var userMessage = new UserMessage
                {
                    ReceiverId = groupName,
                    MessageId = messageId,
                    Status = false
                };
                _context.UserMessages.Add(userMessage);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CreateUserMessageAsync(string messageId)
        {
            try
            {
                var admin = _userMessagesRepository.GetAllAdminUsersAsync().Result;
                foreach (var user in admin)
                {
                    var userMessage = new UserMessage
                    {
                        ReceiverId = user.Id,
                        MessageId = messageId,
                        Status = false
                    };
                    _context.UserMessages.Add(userMessage);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateUserMessage(string receiverId)
        {
            try
            {
                var userMessage = GetUserMessage(receiverId);
                foreach (var item in userMessage)
                {
                    item.Status = true;
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private List<UserMessage> GetUserMessage(string receiverId)
        {
            try
            {
                return _context.UserMessages.Where(m => m.ReceiverId.Equals(receiverId)).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
