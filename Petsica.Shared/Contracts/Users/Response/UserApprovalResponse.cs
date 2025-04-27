namespace Petsica.Shared.Contracts.Users.Response
{
    public record UserApprovalResponse(
string UserId,
string Email,
string UserName,
string Photo,
string Address,
string Phone,
string NationalID,
string Type,
string ApprovalPhoto
);
}
