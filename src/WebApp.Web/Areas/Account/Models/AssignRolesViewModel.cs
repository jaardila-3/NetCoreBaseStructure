namespace WebApp.Web.Areas.Account.Models;

public class AssignRolesViewModel
{
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public IList<string> UserRoles { get; set; } = []; // User roles
    public IList<string?>? AllRoles { get; set; } = []; // All roles
    public IList<string> SelectedRoles { get; set; } = [];  // Selected roles
}