

namespace Petsica.Core.Entities.Messages;
public class ClinicMessageClinic
{
	public int MessageID { get; set; }
	public int ClinicReceiverID { get; set; }
	public int? ClinicSenderID { get; set; }
	public string Content { get; set; }
	public DateTime Date { get; set; }

	#region Navigation Properties
	public Clinic Receiver { get; set; }
	public Clinic Sender { get; set; }
	#endregion
}
