namespace ChatApp.Domain.Entities;

public class Message
{
    public int MessageId { get; set; }
    public int? GroupId { get; set; }
    public int SenderId { get; set; }
    public string MessageContent { get; set; } = string.Empty;
    public DateTime DateAndTimeSent { get; set; }
    public bool IsDeleted { get; set; }
    public int ReceiverId { get; set; }
}
