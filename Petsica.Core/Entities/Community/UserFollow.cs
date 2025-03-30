using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Petsica.Core.Entities.Community;
public class UserFollow
{
	public int Id { get; set; }


	[ForeignKey("FollowerUser")]
	public string UserId { get; set; }

	[JsonIgnore]
	public User User { get; set; }

	[ForeignKey("FollowedUser")]
	public string FollowedUserId { get; set; }
	[JsonIgnore]
	public User FollowedUser { get; set; }


}