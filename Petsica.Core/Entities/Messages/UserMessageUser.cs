

namespace Petsica.Core.Entities.Messages;
public class UserMessageUser
{
	public int MessageID { get; set; }
	public int UserReceiverID { get; set; }
	public int UserSenderID { get; set; }
	public string Content { get; set; }
	public DateTime Date { get; set; }

	#region Navigation Properties
	public User Receiver { get; set; }
	public User Sender { get; set; }
	#endregion
}
