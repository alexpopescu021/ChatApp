using ChatApp.BusinessLogic.DTOs.Message;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;

namespace ChatApp.BusinessLogic.Services;

public class MessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public MessageDto? Get(int id)
    {
        var message = _messageRepository.Get(id);
        if (message == null) return null;

        return new MessageDto
        {
            MessageId = message.MessageId,
            SenderId = message.SenderId,
            MessageContent = message.MessageContent,
            DateAndTimeSent = message.DateAndTimeSent,
            IsDeleted = message.IsDeleted,
        };
    }

    public IEnumerable<MessageDto> GetAvailableMessagesBySenderId(int senderId)
    {
        var messages = _messageRepository.GetMessagesBySenderId(senderId);

        return messages.Where(m => !m.IsDeleted)
                       .Select(m => new MessageDto
                        {
                            MessageId = m.MessageId,
                            SenderId = m.SenderId,
                            MessageContent = m.MessageContent,
                            DateAndTimeSent = m.DateAndTimeSent,
                            IsDeleted = m.IsDeleted
                        });
    }

    public int Add(MessageBaseDto messageDto)
    {
        var message = new Message
        {
            SenderId = messageDto.SenderId,
            MessageContent = messageDto.MessageContent,
            DateAndTimeSent = messageDto.DateAndTimeSent
        };

        return _messageRepository.Add(message);
    }

    public void Update(int id, MessageBaseDto messageDto)
    {
        var message = new Message
        {
            MessageContent = messageDto.MessageContent
        };

        _messageRepository.Update(id, message);
    }

    public void Delete(int id)
    {
        _messageRepository.Delete(id);
    }

    public void SoftDelete(int id)
    {
        _messageRepository.SoftDelete(id);
    }

    public IEnumerable<NotificationDto> GetNotifications(int senderId)
    {
        var messages = _messageRepository.GetNotifications(senderId);
        _messageRepository.NotificationRead(senderId);

        return messages.Where(m => !m.IsDeleted)
            .Select(m => new NotificationDto()
            {
                MessageId = m.MessageId,
                Content = m.MessageContent.Length > 30 ? m.MessageContent.Substring(0, Math.Min(m.MessageContent.Length, 30)) + "..." : m.MessageContent
            });
    }
}
