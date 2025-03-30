namespace Petsica.Core.Entities.Users;
public class Clinic
{
    public string ClinicID { get; set; }
    public string WorkingHours { get; set; }
    public string ContactInfo { get; set; }

    #region Navigation Properties
    // public List<ClinicApproval> Approvals { get; set; }
    public List<ClinicMessageClinic> ClinicMessages { get; set; }
    public List<UserMessageClinic> UserMessages { get; set; }
    #endregion
}
