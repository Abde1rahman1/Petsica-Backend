

using System.ComponentModel.DataAnnotations.Schema;

namespace Petsica.Core.Entities.Messages;
public class ClinicMessageClinic
{
    public int MessageID { get; set; }
    public string ClinicReceiverID { get; set; }
    public string? ClinicSenderID { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;

	#region Navigation Properties
	[NotMapped]
	public Clinic Receiver { get; set; }
	[NotMapped]
	public Clinic Sender { get; set; }
    #endregion
}
