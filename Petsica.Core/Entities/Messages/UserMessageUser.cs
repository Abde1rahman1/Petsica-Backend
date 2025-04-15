

namespace Petsica.Core.Entities.Messages;
public class UserMessageUser
{
    public int MessageID { get; set; }
    public string UserReceiverID { get; set; }
    public string UserSenderID { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }= DateTime.Now;

    #region Navigation Properties
    public User Receiver { get; set; }
    public User Sender { get; set; }
    #endregion
}
