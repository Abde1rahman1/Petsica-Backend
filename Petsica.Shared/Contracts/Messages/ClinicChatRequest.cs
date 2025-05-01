using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Messages;
public record ClinicChatRequest
(
	string ClinicReceiverID,
	string ClinicSenderID,
	string Content,
	string Date
	);
