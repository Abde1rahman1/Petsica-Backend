namespace Petsica.Shared.Contracts.Users.Response
{
    public record ClinicApprovalResponse(
   string UserId,
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
