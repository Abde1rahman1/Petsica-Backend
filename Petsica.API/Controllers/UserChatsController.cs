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
	public async Task<IActionResult> SendMessage( ChatUsersReques message)
	{
		
		await _chatService.SaveMessageAsync(message);

		await _hubContext.Clients.User(message.UserReceiverID)
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

