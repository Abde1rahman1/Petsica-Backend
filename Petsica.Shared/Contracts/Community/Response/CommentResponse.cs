using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Community.Response;
public record CommentResponse
(
	int CommentId,
	string Content,
	string UserID
);
