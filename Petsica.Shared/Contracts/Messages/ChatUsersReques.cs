using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Messages;
public record ChatUsersReques
(
	 string UserReceiverID,
	string UserSenderID,
	string Content
);
