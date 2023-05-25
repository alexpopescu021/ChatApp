namespace ChatApp.BusinessLogic.DTOs.Message;

public class MessageBaseDto
{
    public int SenderId { get; set; }
    public string MessageContent { get; set; } = string.Empty;
    public DateTime DateAndTimeSent { get; set; }
    public bool IsDeleted { get; set; }
}
