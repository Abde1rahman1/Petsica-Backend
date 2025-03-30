namespace Petsica.Shared.Contracts.Authrization.Request
{
    public record RegisterRequest(
    string UserName,
    string Email,
    string Password,
    string Photo,
    string Address,
    string Type,
    string ApprovalPhoto,
    string NationalID
);
    public record ClinicRegisterRequest(
   string UserName,
   string Email,
   string Password,
   string Photo,
   string Address,
   string ApprovalPhoto,
   string Type,
   string WorkingHours,
   string ContactInfo,
   string NationalID
);
}
