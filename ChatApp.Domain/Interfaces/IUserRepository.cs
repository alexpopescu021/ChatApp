using ChatApp.Domain.Entities;

namespace ChatApp.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public User? GetByEmailAndPassword(string email, string password);

    public void UpdateLastLoginDate(int userId, DateTime lastLoginDate);
}
