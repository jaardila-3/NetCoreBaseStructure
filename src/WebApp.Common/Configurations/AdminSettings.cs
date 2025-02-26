namespace WebApp.Common.Configurations;

public class AdminSettings
{
    public string? Email { get; set; }
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentificationNumber { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? Dependency { get; set; }
}