namespace ChatApp.BusinessLogic.DTOs.User;

public class UserLoginDto
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public bool LoginFailed { get; set; } = false;
}
