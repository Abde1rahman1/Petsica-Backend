

namespace Petsica.Core.Entities.Messages;
public class UserMessageClinic
{
    public int MessageID { get; set; }
    public string UserID { get; set; }
    public string ClinicID { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }

    #region Navigation Properties
    public User User { get; set; }
    public Clinic Clinic { get; set; }
    #endregion
}
