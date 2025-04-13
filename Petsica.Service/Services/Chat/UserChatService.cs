using Petsica.Core.Entities.Messages;
using Petsica.Service.Abstractions.Messages;
using Petsica.Shared.Contracts.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Services.Chat;
public class UserChatService : IUserChatService
{
	private readonly ApplicationDbContext _context;

	public UserChatService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task SaveMessageAsync(ChatUsersReques message)
	{
		var msg = new UserMessageUser
		{
			UserReceiverID=message.UserReceiverID,
			UserSenderID=message.UserSenderID,
			Date = DateTime.Now,
			Content = message.Content
		};

		_context.UserMessageUsers.Add(msg);
		await _context.SaveChangesAsync();
	}

	public async Task<List<UserMessageResponse>> GetMessagesAsync(string user1Id, string user2Id)
	{
		var messages = await _context.UserMessageUsers
			.Where(m =>
				(m.UserSenderID == user1Id && m.UserReceiverID == user2Id) ||
				(m.UserSenderID == user2Id && m.UserReceiverID == user1Id))
			.OrderBy(m => m.Date)
			.ToListAsync();

		var response = messages.Select(m => new UserMessageResponse(
			m.MessageID,
			m.UserSenderID,
			m.UserReceiverID,
			m.Content,
			m.Date
		)).ToList();

		return response;
	}
}
