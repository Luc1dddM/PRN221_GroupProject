using Microsoft.AspNetCore.SignalR;

namespace PRN221_GroupProject.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            var user = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;
            await Clients.All.SendAsync("ReceiveMessage", user, message, connectionId);
        }
    }
}
/*public async Task SendMessageToAdmin(string userid, string message)
        {
            await Clients.Users(userid).SendAsync("ReceiveMessage", message);
        }*/