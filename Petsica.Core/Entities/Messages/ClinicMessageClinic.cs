

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    [JsonIgnore]
	public Clinic Receiver { get; set; }
	[NotMapped]
    [JsonIgnore]
    public Clinic Sender { get; set; }
    #endregion
}
