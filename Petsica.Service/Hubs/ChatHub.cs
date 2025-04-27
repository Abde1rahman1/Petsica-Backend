using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Hubs;
public class ChatHub : Hub
{
    public async Task SendMessage(string senderId, string receiverId, string message)
    {
        await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message, DateTime.Now);
    }
}
