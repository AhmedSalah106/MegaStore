using Microsoft.AspNetCore.SignalR;

namespace MegaStore.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string SenderId,string ReceiverId,string Message)
        {
            await Clients.User(ReceiverId).SendAsync("ReceiveMessage",SenderId, Message);
        }
    }
}
