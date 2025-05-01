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
    Task SendMessageAsync(ClinicChatRequest request);
    Task<IEnumerable<ClinicMessageClinic>> GetMessagesAsync(string clinicReceiverID, string clinicSenderID);
}

