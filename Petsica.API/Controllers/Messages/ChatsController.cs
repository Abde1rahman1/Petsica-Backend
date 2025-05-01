using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Petsica.Core.Entities.Messages;
using Petsica.Service.Abstractions.Messages;
using Petsica.Shared.Contracts.Messages;
using Petsica.Shared.Hubs;

namespace Petsica.API.Controllers.Messages;
[Route("api/[controller]")]
[ApiController]
public class ChatsController : ControllerBase
{
    private readonly IClinicChatService _clinicChatService;
    private readonly IHubContext<ClinicChatHub> _hubContext;
    public ChatsController(IClinicChatService clinicChatService, IHubContext<ClinicChatHub> hubContext)
    {
        _clinicChatService = clinicChatService;
        _hubContext = hubContext;
    }


    


    [HttpPost("send-message")]
    public async Task<IActionResult> SendMessage([FromBody] ClinicChatRequest request)
    {
        await _clinicChatService.SendMessageAsync(request);
        await _hubContext.Clients.User(request.ClinicReceiverID).SendAsync("ReceiveMessage", request);
        return Ok();
    }

    [HttpGet("get-messages/{clinicReceiverID}/{clinicSenderID}")]
    public async Task<IActionResult> GetMessages( string clinicReceiverID, string clinicSenderID)
    {
        var messages = await _clinicChatService.GetMessagesAsync(clinicReceiverID, clinicSenderID);
        return Ok(messages);
    }
}
