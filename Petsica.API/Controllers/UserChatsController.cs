using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Petsica.Service.Abstractions.Messages;
using Petsica.Shared;
using Petsica.Shared.Contracts.Messages;

namespace Petsica.API.Controllers;
[EnableCors("AllowFrontend")]
[Route("api/[controller]")]
[ApiController]
public class UserChatsController : ControllerBase
{
	private readonly IUserChatService _chatService;
	private readonly IHubContext<UserChatHub> _hubContext;

	public UserChatsController(IUserChatService chatService, IHubContext<UserChatHub> hubContext)
	{
		_chatService = chatService;
		_hubContext = hubContext;
	}

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage(ChatUsersReques message)
    {
        // Save the message to the database
        await _chatService.SaveMessageAsync(message);

        // Send the message to both the sender and receiver (real-time)
        await _hubContext.Clients.Users(message.UserSenderID, message.UserReceiverID)
            .SendAsync("ReceiveMessage", message.UserSenderID, message.Content, DateTime.UtcNow);

        return Ok();
    }



    [HttpGet("messages/{user1Id}/{user2Id}")]
	public async Task<IActionResult> GetChatHistory(string user1Id, string user2Id)
	{
		var messages = await _chatService.GetMessagesAsync(user1Id, user2Id);
		return Ok(messages);
	}
}

