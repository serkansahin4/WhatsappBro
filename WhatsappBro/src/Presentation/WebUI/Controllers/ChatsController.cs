using Application.DTOs;
using Application.Features.UserFriendFeatures.GetUserFriendDetails;
using Application.Features.UserMessageFeatures;
using Application.Features.UserMessageFeatures.GetUserFriendMessageByIdFeature;
using Application.Features.UserMessageFeatures.GetUserFriendMessageByMessageIdFeature;
using Application.Features.UserMessageFeatures.GetUserFriendMessagesFeature;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ChatsController(IMediator mediator, IHttpContextAccessor contextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _contextAccessor = contextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Messages()
        {
            string authUserName = User.Identity.Name;
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");

            List<UserFriendMessageDto> friendMessages = await _mediator.Send(new GetUserFriendMessageQuery
            {
                UserName = authUserName,
                RootPath= path
            });

            return Ok(friendMessages);
        }
        [HttpGet]
        [Route("{friendId}")]
        public async Task<IActionResult> Messages(string friendId)
        {
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            List<UserMessageDto> userMessages = await _mediator.Send(new GetUserFriendMessageByIdQuery
            {
                FriendId = friendId,
                RootPath= path
            });
            return Ok(userMessages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageAsync([FromForm] CreateUserMessageCommand createUserMessageCommand)
        {
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            createUserMessageCommand.RootPath = path;
            Guid messageId = await _mediator.Send(createUserMessageCommand);
            
            UserFriendMessageDto message= await _mediator.Send(new GetUserFriendMessageByMessageIdQuery
            {
                 MessageId=messageId.ToString(),
                 RootPath=path
            });
            return Ok(message);
        }



    }
}
