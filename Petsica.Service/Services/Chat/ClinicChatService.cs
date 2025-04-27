using Petsica.Core.Entities.Messages;
using Microsoft.EntityFrameworkCore;
using Petsica.Service.Abstractions.Messages;
using Petsica.Shared.Contracts.Messages;

public class ClinicChatService : IClinicChatService
{
    private readonly ApplicationDbContext _dbContext;

    public ClinicChatService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SendMessageAsync(ClinicChatRequest request)
    {
        var message = new ClinicMessageClinic
        {
            ClinicReceiverID = request.ClinicReceiverID,
            ClinicSenderID = request.ClinicSenderID,
            Content = request.Content,
            Date = DateTime.Now // Set the Date to the current DateTime
        };

        _dbContext.ClinicMessageClinics.Add(message);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ClinicMessageClinic>> GetMessagesAsync(string clinicReceiverID, string clinicSenderID)
    {
        var messages = await _dbContext.ClinicMessageClinics
            .Where(m => (m.ClinicReceiverID == clinicReceiverID && m.ClinicSenderID == clinicSenderID) ||
                        (m.ClinicReceiverID == clinicSenderID && m.ClinicSenderID == clinicReceiverID))
            .OrderBy(m => m.Date)
            .ToListAsync();

       

        return messages;
    }
}