using Petsica.Core.Entities.Messages;
using Petsica.Service.Abstractions.Messages;
using Petsica.Shared.Contracts.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Services.Chat;
public class ClinicChatService : IClinicChatService
{
	private readonly ApplicationDbContext _context;

	public ClinicChatService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task SaveMessageAsync(ClinicChatRequest message)
	{
		try
		{
			var mesg = new ClinicMessageClinic
			{
				Content = message.Content,
				Date = DateTime.UtcNow,
				ClinicReceiverID = message.ClinicReceiverID,
				ClinicSenderID = message.ClinicSenderID
			};
			_context.ClinicMessageClinics.Add(mesg);
			await _context.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			// Log or print the exception for debugging
			Console.WriteLine("Error saving message: " + ex.Message);
			throw;
		}
	}


	public async Task<List<ClinicMessageClinic>> GetMessagesAsync(string clinic1Id, string clinic2Id)
	{
		return await _context.ClinicMessageClinics
			.Where(m => (m.ClinicSenderID == clinic1Id && m.ClinicReceiverID == clinic2Id)
					 || (m.ClinicSenderID == clinic2Id && m.ClinicReceiverID == clinic1Id))
			.OrderBy(m => m.Date)
			.ToListAsync();
	}
}
