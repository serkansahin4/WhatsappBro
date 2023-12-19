using Application.DTOs;
using Application.Features.UserFeatures.GetUserFriendsFeatures;
using Application.Features.UserFeatures.GetUsersFeatures;
using Application.Features.UserFriendFeatures.InsertUserFriend;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UsersController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("{searchText}")]
        public async Task<IActionResult> Get(string searchText)
        {
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            List<UserDetailDto> userDetailDtos = await _mediator.Send(new GetSearchUsersQuery
            {
                SearchText = searchText,
                RootPath = path,
                UserName = User.Identity.Name
            });
            return Ok(userDetailDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string userId)
        {
            await _mediator.Send(new InsertUserFriendCommand
            {
                UserName = User.Identity.Name,
                FriendId = Guid.Parse(userId)
            });
            return Ok();
        }

        [HttpGet]
        [Route("friends")]
        public async Task<IActionResult> Friends()
        {
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            List<UserDetailDto> userDetailDtos = await _mediator.Send(new GetUserFriendsQuery
            {
                UserName = User.Identity.Name,
                RootPath = path
            });
            return Ok(userDetailDtos);
        }
    }
}
