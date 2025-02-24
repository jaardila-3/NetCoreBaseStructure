namespace WebApp.Web.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public int? ErrorStatusCode { get; set; }
    public string? ErrorMessage { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
