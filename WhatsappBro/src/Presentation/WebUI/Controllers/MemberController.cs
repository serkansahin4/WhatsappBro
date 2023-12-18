using Application.Features.MemberFeatures;
using Application.Features.UserFeatures;
using Core.Helpers.FileHelper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class MemberController : Controller
    {
        readonly IMediator _mediator;
        readonly IWebHostEnvironment _webHostEnvironment;

        public MemberController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Client()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SignUpCommand insertUserCommand)
        {
            string[] extensions = { ".jpg", ".jpeg", ".png" };
            ImageResponse imageResponse= await FileHelper.CreateAsync(Request.Form.Files[0], "userprofiles\\path\\", "userprofiles\\thumnailpath\\", 152222, extensions);
            insertUserCommand.Path = imageResponse.Path;
            insertUserCommand.ThumbnailPath=imageResponse.ThumbnailPath;
            await _mediator.Send(insertUserCommand);
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInCommand signInCommand)
        {
            await _mediator.Send(signInCommand);
            return RedirectToAction("Client");
        }

        public IActionResult UnAuthorized()
        {
            return View();
        }
    }
}
