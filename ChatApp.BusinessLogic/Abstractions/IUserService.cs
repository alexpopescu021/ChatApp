using ChatApp.BusinessLogic.DTOs.User;

namespace ChatApp.BusinessLogic.Abstractions;

public interface IUserService
{
    public UserDto? Get(int id);
    public void Update(int id, UserBaseDto user);
    public bool Login(UserLoginDto userDto);
    public void Delete(int id);
    public int Add(UserBaseDto userBaseDto);

}

