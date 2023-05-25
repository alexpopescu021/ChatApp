namespace ChatApp.Domain.Entities;

public class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime DateOfRegistration { get; set; }

    public bool IsDisabled { get; set; }

    public DateTime? LastLoginDate { get; set; }
}
