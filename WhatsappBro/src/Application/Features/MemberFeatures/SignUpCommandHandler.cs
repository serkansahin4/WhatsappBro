using Application.Constants;
using Application.Exceptions;
using AutoMapper;
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
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand>
    {
        readonly IMapper _mapper;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        public SignUpCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            AppUser appUser = _mapper.Map<SignUpCommand, AppUser>(request);
            IdentityResult identityResult = await _userManager.CreateAsync(appUser,request.Password);
            if (!identityResult.Succeeded)
            {
                throw new ValidationException(ExceptionMessage.User.NotCreatedUser);
            }
        }
    }
}
