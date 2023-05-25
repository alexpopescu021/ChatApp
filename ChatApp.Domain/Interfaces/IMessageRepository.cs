using ChatApp.Domain.Entities;

namespace ChatApp.Domain.Interfaces;

public interface IMessageRepository : IRepository<Message>
{
    public void SoftDelete(int id);
    public IEnumerable<Message> GetMessagesBySenderId(int senderId);
    public IEnumerable<Message> GetNotifications(int receiverId);
    public void NotificationRead(int receiverId);

}
