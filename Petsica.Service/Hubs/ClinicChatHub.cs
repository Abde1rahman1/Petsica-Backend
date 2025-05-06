using Microsoft.AspNetCore.SignalR;
using Petsica.Service.Abstractions.Messages;
using Petsica.Shared.Contracts.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Hubs;


public class ClinicChatHub : Hub
{
    private readonly IClinicChatService _clinicChatService;

    public ClinicChatHub(IClinicChatService clinicChatService)
    {
        _clinicChatService = clinicChatService;
    }

    public async Task SendMessage(ClinicChatRequest request)
    {
        try
        {
            Console.WriteLine($"Received message from {request.ClinicSenderID} to {request.ClinicReceiverID}: {request.Content}");

            // Send the message to the database
            await _clinicChatService.SendMessageAsync(request);

            // Send the message to the receiver
            await Clients.User(request.ClinicReceiverID).SendAsync("ReceiveMessage", request);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SendMessage Error: {ex.Message}");
            throw; 
        }
    }

    public async Task GetMessages(string clinicReceiverID, string clinicSenderID)
    {
        var messages = await _clinicChatService.GetMessagesAsync(clinicReceiverID, clinicSenderID);
        await Clients.Caller.SendAsync("ReceiveMessages", messages);
    }
}