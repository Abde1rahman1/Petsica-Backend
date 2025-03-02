using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Community;
public record PostResponse
(
	int Id,
	string Content,
	string userId,
	DateTime Date,
	string Photo
);
