using ChatApp.BusinessLogic.DTOs.User;
using ChatApp.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;


namespace ChatApp.WebApi.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : Controller
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        var user = _userService.Get(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status401Unauthorized)]
    public ActionResult Login([FromForm] UserLoginDto userLoginDto)
    {
        var success = _userService.Login(userLoginDto.Email, userLoginDto.Password);

        if (success != null)
        {
            return Unauthorized();
        }

        userLoginDto.LoginFailed = true;
        return Ok(success.Value);
    }

    [HttpPost("login-sql-injection")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public IActionResult LoginSqlInjection([FromBody] UserLoginDto userLoginDto)
    {
        var userDto = _userService.LoginSqlInjection(userLoginDto.Email, userLoginDto.Password);

        return userDto != null ? Ok(userDto) : Unauthorized();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserBaseDto), StatusCodes.Status201Created)]
    public IActionResult Post([FromBody] UserBaseDto userDto)
        => CreatedAtAction(nameof(Get), new { id = _userService.Add(userDto) }, userDto);


    [HttpPut]
    [Route("{id}")]
    public IActionResult Put(int id, [FromBody] UserBaseDto userDto)
    {
        _userService.Update(id, userDto);
        return Ok();
    }
}
