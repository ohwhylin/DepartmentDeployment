namespace GatewayApi.Auth;

public class AuthProfileDto
{
    public bool Exists { get; set; }
    public bool IsActive { get; set; }
    public string Login { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
}