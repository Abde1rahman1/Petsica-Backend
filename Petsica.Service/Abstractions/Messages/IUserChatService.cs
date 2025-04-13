using Petsica.Core.Entities.Messages;
using Petsica.Shared.Contracts.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Abstractions.Messages;
public interface IUserChatService
{
	Task SaveMessageAsync(ChatUsersReques message);
	Task<List<UserMessageResponse>> GetMessagesAsync(string user1Id, string user2Id);
}
