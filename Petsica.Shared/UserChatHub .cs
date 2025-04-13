using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared;
public class UserChatHub : Hub
{
	public async Task SendMessage(string senderId, string receiverId, string content)
	{
		var message = new
		{
			SenderId = senderId,
			Content = content,
			Date = DateTime.UtcNow
		};

		await Clients.User(receiverId).SendAsync("ReceiveMessage", message);
	}
}
