﻿using Application.Features.MemberFeatures;
using Application.Features.UserFeatures;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class MemberController : Controller
    {
        readonly IMediator _mediator;

        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]InsertUserCommand insertUserCommand)
        {
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
        public IActionResult Login([FromBody]SignInCommand signInCommand)
        {
            return View();
        }
    }
}