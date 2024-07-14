using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Plugins;
using PRN221_GroupProject.Hubs;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Messages;
using PRN221_GroupProject.Repository.UserMessages;
using PRN221_GroupProject.Repository.Users;

namespace PRN221_GroupProject.Pages.Admin.Chat
{
    public class ChatPageModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserMessagesRepository _userMessagesRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        [BindProperty]
        public string GroupName { get; set; }
        [BindProperty]
        public PRN221_GroupProject.Models.Message message { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public string user { get; set; }

        public ChatPageModel(IUserRepository userRepository, 
                             IMessageRepository messageRepository, 
                             IUserMessagesRepository userMessagesRepository,
                             UserManager<ApplicationUser> userManager,
                             IHubContext<ChatHub> hubContext)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _userMessagesRepository = userMessagesRepository;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> OnGet(string? id)
        {
            GroupName = id;
            user = _userManager.GetUserId(User);
            Users = await _userRepository.GetUsersAsync();
            if (id != null)
            {
                _userMessagesRepository.UpdateUserMessage(id, user);
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                
                Users = await _userRepository.GetUsersAsync();
                var senderId = _userManager.GetUserId(User);
                user = _userManager.GetUserId(User);
                _messageRepository.CreateMessage(senderId, GroupName, message);
                _userMessagesRepository.CreateAdminUserMessageAsync(message.MessageId, GroupName);
                await _hubContext.Clients.All.SendAsync("LoadMessage");
                await _hubContext.Clients.All.SendAsync("LoadMessageNotification");

                return Page();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IActionResult> OnGetMessage(string groupName, string receiverId)
        {
            try
            {
                if (groupName != null)
                {
                    _userMessagesRepository.UpdateUserMessage(groupName, receiverId);
                    await _hubContext.Clients.All.SendAsync("LoadForAdminNotification");
                    await _hubContext.Clients.All.SendAsync("LoadMessageNotification");
                }
                var message = _messageRepository.GetAllMessage(groupName);
                return new JsonResult(message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public async Task<IActionResult> OnGetAdminNotification(string receiver)
        {
            try
            {
                var message = await _userMessagesRepository.CountMessagesUnReadSpecific(receiver);
                return new JsonResult(message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
