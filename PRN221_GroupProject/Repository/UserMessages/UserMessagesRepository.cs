using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Messages;
using PRN221_GroupProject.Repository.Users;

namespace PRN221_GroupProject.Repository.UserMessages
{
    public class UserMessagesRepository : IUserMessagesRepository
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public UserMessagesRepository(Prn221GroupProjectContext context, IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }
        public int CountMessagesUnReadAdmin(string receiver)
        {
            try
            {
                return _context.Messages.Include(m=>m.UserMessages).Count(m => m.UserMessages.Any(u => u.ReceiverId.Equals(receiver) && !u.Status));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int CountMessagesUnReadCustomer(string receiver, string groupName)
        {
            try
            {
                return _messageRepository.GetAllMessage(groupName).Where(c => c.UserMessages.Any(u => u.ReceiverId.Equals(receiver) && !u.Status)).Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<MessageDTO>> CountMessagesUnReadSpecific(string receiver)
        {
            try
            {
                var user = await _userRepository.GetAllCustomerUsersAsync();
                var message = new List<MessageDTO>();
                foreach (var item in user)
                {
                    var tmp = _messageRepository.GetAllMessage(item.Id)
                                         .Where(c => c.UserMessages.Any(u => u.ReceiverId.Equals(receiver) && !u.Status) && c.SenderId.Equals(item.Id)).Count();
                    message.Add(new MessageDTO()
                    {
                        users = item,
                        notification = tmp
                    });
                }
                return message;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
                var admin = _userRepository.GetAllAdminUsersAsync().Result;
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

        public void UpdateUserMessage(string groupName, string receiverId)
        {
            try
            {
                var userMessage = GetUserMessage(groupName, receiverId);
                foreach (var item in userMessage)
                {
                    item.UserMessages.First(c=>c.ReceiverId.Equals(receiverId)).Status = true;
                    
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private List<PRN221_GroupProject.Models.Message> GetUserMessage(string groupName, string receiverId)
        {
            try
            {
                return _messageRepository.GetAllMessage(groupName).Where(c=>c.UserMessages.Any(c=>c.ReceiverId.Equals(receiverId))).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
