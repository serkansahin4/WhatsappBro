using Application.Constants;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.UserFeatures
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public InsertUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            AppUser? appUser= _userManager.Users.SingleOrDefault(x => x.UserName == request.UserName);
            if (appUser is not null)
            {
                throw new AlreadyUserExistException(ExceptionMessage.User.AlreadyUserExist);
            }

            appUser = _mapper.Map<AppUser>(request);
            await _userManager.CreateAsync(appUser);
        }
    }
}
