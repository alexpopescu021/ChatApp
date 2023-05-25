using ChatApp.BusinessLogic.DTOs.User;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;

namespace ChatApp.BusinessLogic.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserDto? Get(int id)
    {
        var user = _userRepository.Get(id);
        if (user == null) return null;

        return new UserDto
        {
            UserId = user.UserId,
            Email = user.Email,
            Password = user.Password,
            Username = user.Username,
            DateOfRegistration = user.DateOfRegistration,
            IsDisabled = user.IsDisabled,
            LastLoginDate = user.LastLoginDate
        };
    }

    public int? Login(string email, string password)
    {
        var user = _userRepository.GetByEmailAndPassword(email, password);

        if (user != null && !user.IsDisabled)
        {
            _userRepository.UpdateLastLoginDate(user.UserId, DateTime.Now);

            return user.UserId;
        }

        return null;
    }

    public UserDto? LoginSqlInjection(string email, string password)
    {
        UserDto? userDto = null;
        var user = _userRepository.GetByEmailAndPassword(email, password);

        if (user != null && !user.IsDisabled)
        {
            _userRepository.UpdateLastLoginDate(user.UserId, DateTime.Now);

            userDto = new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Username = user.Username,
                DateOfRegistration = user.DateOfRegistration,
                Password = string.Empty,
                IsDisabled = user.IsDisabled,
                LastLoginDate = user.LastLoginDate
            };
        }

        return userDto;
    }

    public void Delete(int id)
    {
        _userRepository.Delete(id);
    }

    public int Add(UserBaseDto userDto)
    {
        var user = new User
        {
            Email = userDto.Email,
            Password = userDto.Password,
            Username = userDto.Username,
            DateOfRegistration = userDto.DateOfRegistration,
            IsDisabled = userDto.IsDisabled,
            LastLoginDate = userDto.LastLoginDate
        };

        return _userRepository.Add(user);
    }

    public void Update(int id, UserBaseDto userDto)
    {
        var user = new User
        {
            Email = userDto.Email,
            Password = userDto.Password,
            Username = userDto.Username,
            DateOfRegistration = userDto.DateOfRegistration,
            IsDisabled = userDto.IsDisabled,
            LastLoginDate = userDto.LastLoginDate
        };

        _userRepository.Update(id, user);
    }
}
