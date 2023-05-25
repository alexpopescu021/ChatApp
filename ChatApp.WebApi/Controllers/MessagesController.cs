using ChatApp.BusinessLogic.DTOs.Message;
using ChatApp.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.WebApi.Controllers;

[Route("api/messages")]
[ApiController]
public class MessagesController : Controller
{
    private readonly MessageService _messageService;

    public MessagesController(MessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        var message = _messageService.Get(id);
        return message == null ? NotFound() : Ok(message);
    }

    [HttpGet("sender/{senderId}")]
    [ProducesResponseType(typeof(IEnumerable<MessageDto>), StatusCodes.Status200OK)]
    public ActionResult GetBySenderId(int senderId)
    {
        return Ok(_messageService.GetAvailableMessagesBySenderId(senderId));
    }

    [HttpPost]
    [ProducesResponseType(typeof(MessageBaseDto), StatusCodes.Status201Created)]
    public IActionResult Post([FromBody] MessageBaseDto messageDto)
        => CreatedAtAction(nameof(Get), new { id = _messageService.Add(messageDto) }, messageDto);


    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public IActionResult Put(int id, [FromBody] MessageBaseDto messageDto)
    {
        _messageService.Update(id, messageDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public IActionResult Delete(int id)
    {
        _messageService.Delete(id);
        return Ok();
    }

    [HttpDelete("soft/{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public IActionResult SoftDelete(int id)
    {
        _messageService.SoftDelete(id);
        return Ok();
    }

    // Make separate logic for notifications? model/controller etc. Would it be useful in the future
    [HttpGet("notifications/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<NotificationDto>), StatusCodes.Status200OK)]
    public IActionResult ReceiveNotification(int userId)
    {
        return Ok(_messageService.GetNotifications(userId));

    }

}
