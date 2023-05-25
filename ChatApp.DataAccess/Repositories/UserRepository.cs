using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;

using System.Data.SqlClient;

namespace ChatApp.DataAccess.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(string connectionString) : base(connectionString)
    {
    }

    public override User? Get(int id)
    {
        User? user = null;
        var query = $"SELECT * FROM [User] WHERE UserId = {id}";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            user = new User();
            user.UserId = (int)reader[nameof(user.UserId)];
            user.Username = (string)reader[nameof(user.Username)];
            user.Password = (string)reader[nameof(user.Password)];
            user.Email = (string)reader[nameof(user.Email)];
            user.DateOfRegistration = (DateTime)reader[nameof(user.DateOfRegistration)];
            user.IsDisabled = (bool)reader[nameof(user.IsDisabled)];
            user.LastLoginDate = reader[nameof(user.LastLoginDate)] is DBNull ? null : (DateTime)reader[nameof(user.LastLoginDate)];
        }

        _connection.Close();

        return user;
    }

    public User? GetByEmailAndPassword(string email, string password)
    {
        User? user = null;
        var query = $"SELECT * FROM [User] WHERE Email = '{email}' AND Password = '{password}'";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            user = new User
            {
                UserId = (int)reader[nameof(user.UserId)],
                Username = (string)reader[nameof(user.Username)],
                Password = (string)reader[nameof(user.Password)],
                Email = (string)reader[nameof(user.Email)],
                DateOfRegistration = (DateTime)reader[nameof(user.DateOfRegistration)],
                IsDisabled = (bool)reader[nameof(user.IsDisabled)],
                LastLoginDate = reader[nameof(user.LastLoginDate)] is DBNull ? null : (DateTime)reader[nameof(user.LastLoginDate)],
            };
        }

        _connection.Close();

        return user;
    }

    public void UpdateLastLoginDate(int userId, DateTime lastLoginDate)
    {
        var query = $"UPDATE [User] SET LastLoginDate = '{lastLoginDate}' WHERE UserId = {userId}";
        var command = new SqlCommand(query, _connection);

        _connection.Open();
        command.ExecuteNonQuery();
        _connection.Close();
    }

    public override void Delete(int id)
    {
        var query = $"DELETE FROM [User] WHERE UserId = {id}";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        command.ExecuteReader();

        _connection.Close();
    }

    public override int Add(User entity)
    {
        var query = $"INSERT INTO [User] ([Username], [Password], [Email], [DateOfregistration], [IsDisabled], [LastLoginDate]) " +
                    $"OUTPUT INSERTED.UserId " +
                    $"VALUES ('{entity.Username}', '{entity.Password}', '{entity.Email}', " +
                    $"'{entity.DateOfRegistration}', '{entity.IsDisabled}', '{entity.LastLoginDate}')";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        var id = (int)command.ExecuteScalar();

        _connection.Close();

        return id;
    }

    public override void Update(int id, User entity)
    {
        var query = $"UPDATE [User] SET " +
                    $"[Username] = '{entity.Username}', " +
                    $"[Password] = '{entity.Password}', " +
                    $"[Email] = '{entity.Email}', " +
                    $"[DateOfregistration] = '{entity.DateOfRegistration}', " +
                    $"[IsDisabled] = '{entity.IsDisabled}', " +
                    $"[LastLoginDate] = '{entity.LastLoginDate}' " +
                    $"WHERE UserId = {id}";
        var command = new SqlCommand(query, _connection);

        _connection.Open();

        command.ExecuteNonQuery();

        _connection.Close();
    }
}
