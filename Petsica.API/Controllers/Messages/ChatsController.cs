using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Petsica.Core.Entities.Messages;
using Petsica.Service.Abstractions.Messages;
using Petsica.Shared;
using Petsica.Shared.Contracts.Messages;

namespace Petsica.API.Controllers.Messages;
[Route("api/[controller]")]
[ApiController]
public class ChatsController : ControllerBase
{
	private readonly IClinicChatService _chatService;
	private readonly IHubContext<ChatHub> _hubContext;

	public ChatsController(IClinicChatService chatService, IHubContext<ChatHub> hubContext)
	{
		_chatService = chatService;
		_hubContext = hubContext;
	}

	[HttpPost("send")]
	public async Task<IActionResult> SendMessage(ClinicChatRequest message)
	{
		await _chatService.SaveMessageAsync(message);
		await _hubContext.Clients.User(message.ClinicReceiverID)
			.SendAsync("ReceiveMessage", message.ClinicSenderID, message.Content, DateTime.UtcNow);

		return Ok();
	}

	[HttpGet("history")]
	public async Task<IActionResult> GetChatHistory(string clinic1Id, string clinic2Id)
	{
		var messages = await _chatService.GetMessagesAsync(clinic1Id, clinic2Id);
		return Ok(messages);
	}
}
