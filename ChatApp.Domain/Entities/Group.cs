namespace ChatApp.Domain.Entities;

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CreatorId { get; set; }
}
