

namespace Petsica.Core.Entities.Services;
public class SitterService
{
    public int ServiceID { get; set; }
    public string SitterID { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public string Location { get; set; }
    public bool IsDelete { get; set; } = false;
    public bool IsActive { get; set; }

    #region Navigation Property
    public User? Sitter { get; set; }
    public List<UserRequestService>? Requests { get; set; }
    #endregion
}
