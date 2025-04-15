using Petsica.Core.Entities.Messages;
using Petsica.Shared.Contracts.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Abstractions.Messages;
public interface IClinicChatService
{
	Task SaveMessageAsync(ClinicChatRequest message);
	Task<List<ClinicMessageClinic>> GetMessagesAsync(string clinic1Id, string clinic2Id);
}

public class ClinicChatService : IClinicChatService
{
	private readonly ApplicationDbContext _context;

	public ClinicChatService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task SaveMessageAsync(ClinicChatRequest message)
	{
		//message.Date = DateTime.UtcNow;
		var mesg = new ClinicMessageClinic
		{
			Content= message.Content,
			Date = DateTime.UtcNow,
			ClinicReceiverID = message.ClinicReceiverID,
			ClinicSenderID = message.ClinicSenderID
		};
		_context.ClinicMessageClinics.Add(mesg);
		await _context.SaveChangesAsync();
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

