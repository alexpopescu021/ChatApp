using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;
using System.Data.SqlClient;

namespace ChatApp.DataAccess.Repositories;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(string connectionString) : base(connectionString)
    {
    }

    public override int Add(Message entity)
    {
        var query = $"INSERT INTO [Message] ([SenderId], [MessageContent], [DateAndTimeSent]) " +
                    $"OUTPUT INSERTED.MessageId " +
                    $"VALUES ('{entity.SenderId}', '{entity.MessageContent}', '{entity.DateAndTimeSent}')";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        var id = (int)command.ExecuteScalar();

        _connection.Close();

        return id;
    }

    public override void Delete(int id)
    {
        var query = $"DELETE FROM [Message] WHERE MessageId = {id}";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        command.ExecuteReader();

        _connection.Close();
    }

    public override Message? Get(int id)
    {
        Message? message = null;
        var query = $"SELECT * FROM [Message] WHERE MessageId = {id}";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            message = new Message();
            message.SenderId = (int)reader[nameof(message.SenderId)];
            message.MessageContent = (string)reader[nameof(message.MessageContent)];
            message.DateAndTimeSent = (DateTime)reader[nameof(message.DateAndTimeSent)];
            message.IsDeleted = (bool)reader[nameof(message.IsDeleted)];
        }

        _connection.Close();

        return message;
    }

    public IEnumerable<Message> GetMessagesBySenderId(int senderId)
    {
        var messages = Enumerable.Empty<Message>();
        var query = $"SELECT * FROM [Message] WHERE SenderId = {senderId}";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var message = new Message();
            message.SenderId = (int)reader[nameof(message.SenderId)];
            message.MessageContent = (string)reader[nameof(message.MessageContent)];
            message.DateAndTimeSent = (DateTime)reader[nameof(message.DateAndTimeSent)];
            message.IsDeleted = (bool)reader[nameof(message.IsDeleted)];
            messages = messages.Append(message);
        }

        _connection.Close();

        return messages;
    }

    public void SoftDelete(int id)
    {
        var query = $"UPDATE [Message] SET " +
                    $"[IsDeleted] = 1 " +
                    $"WHERE MessageId = {id}";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        command.ExecuteNonQuery();

        _connection.Close();
    }

    public override void Update(int id, Message entity)
    {
        var query = $"UPDATE [Message] SET " +
                    $"[MessageContent] = '{entity.MessageContent}' " +
                    $"WHERE MessageId = {id}";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        command.ExecuteNonQuery();

        _connection.Close();
    }

    public IEnumerable<Message> GetNotifications(int receiverId)
    {
        var messages = Enumerable.Empty<Message>();
        var query = $"SELECT [Message].* FROM [Message] INNER JOIN [User] ON " +
                    $"[Message].receiverId = [User].userId WHERE [Message].receiverId = {receiverId} " +
                    $"AND [User].ReceiveNotifications = 1 AND [Message].NotificationRead = 0";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var message = new Message();
            message.MessageId = (int)reader[nameof(message.MessageId)];
            message.SenderId = (int)reader[nameof(message.SenderId)];
            message.ReceiverId = (int)reader[nameof(message.ReceiverId)];
            message.MessageContent = (string)reader[nameof(message.MessageContent)];
            message.DateAndTimeSent = (DateTime)reader[nameof(message.DateAndTimeSent)];
            message.IsDeleted = (bool)reader[nameof(message.IsDeleted)];
            messages = messages.Append(message);
        }

        _connection.Close();

        return messages;
    }

    public void NotificationRead(int receiverId)
    {
        var query = $"UPDATE [Message] SET NotificationRead = 1 WHERE ReceiverId = {receiverId}";
        var command = new SqlCommand(query, _connection);

        _connection.Open();
        command.ExecuteNonQuery();
        _connection.Close();
    }
}
