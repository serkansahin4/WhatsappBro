using Application.Constants;
using Application.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MemberFeatures
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SignInCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            AppUser appUser = await _userManager.FindByNameAsync(request.UserName);
            if (appUser is null)
            {
                throw new WrongPasswordOrUserNameException(ExceptionMessage.User.WrongPasswordOrUserName);
            }
            SignInResult signResult = await _signInManager.PasswordSignInAsync(appUser, request.Password, false, false);
            if (!signResult.Succeeded)
            {
                throw new WrongPasswordOrUserNameException(ExceptionMessage.User.WrongPasswordOrUserName);
            }
        }
    }
}
