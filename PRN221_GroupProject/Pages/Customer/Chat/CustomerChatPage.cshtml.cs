using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PRN221_GroupProject.Hubs;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Messages;
using PRN221_GroupProject.Repository.UserMessages;
using PRN221_GroupProject.Repository.Users;

namespace PRN221_GroupProject.Pages.Customer.Chat
{
    public class CustomerChatPageModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserMessagesRepository _userMessagesRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        public CustomerChatPageModel(Prn221GroupProjectContext context, 
                                     UserManager<ApplicationUser> userManager,
                                     IUserRepository userRepository,
                                     IMessageRepository messageRepository,
                                     IUserMessagesRepository userMessagesRepository,
                                     IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _userMessagesRepository = userMessagesRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public string SenderId { get; set; }
        [BindProperty]
        public Message Message { get; set; }
        public ApplicationUser Customer { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var customerId = _userManager.GetUserId(User);
            SenderId = customerId;
            Customer = await _userRepository.FindUserByIdAsync(customerId);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var senderId = _userManager.GetUserId(User);
                SenderId = senderId;
                _messageRepository.CreateMessage(senderId, SenderId, Message);
                _userMessagesRepository.CreateUserMessageAsync(Message.MessageId);
                await _hubContext.Clients.All.SendAsync("LoadMessage");
                return Page();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IActionResult OnGetMessage(string groupName)
        {
            try
            {
                var message = _messageRepository.GetAllMessage(groupName);
                return new JsonResult(message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
