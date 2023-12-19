using Application.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFriendFeatures.InsertUserFriend
{
    public class InsertUserFriendCommandHandler : IRequestHandler<InsertUserFriendCommand>
    {
        private readonly IUserFriendRepository _userFriendRepository;
        readonly UserManager<AppUser> _userManager;

        public InsertUserFriendCommandHandler(UserManager<AppUser> userManager, IUserFriendRepository userFriendRepository)
        {
            _userManager = userManager;
            _userFriendRepository = userFriendRepository;
        }

        public async Task Handle(InsertUserFriendCommand request, CancellationToken cancellationToken)
        {
            AppUser user = _userManager.Users.SingleOrDefault(x => x.UserName == request.UserName);
            AppUser friendUser = _userManager.Users.SingleOrDefault(x => x.Id == request.FriendId);
            UserFriend? userFriend = await _userFriendRepository.GetAsync(x => (x.UserId == user.Id && x.FriendId == request.FriendId) || x.UserId == request.FriendId && x.FriendId == user.Id);
            if (userFriend is not null)
            {
                throw new AlreadyFriendException(ExceptionMessage.UserFriend.AlreadyFriend);
            }

            userFriend = new UserFriend { UserId = user.Id, FriendId = friendUser.Id,CreatedDate=DateTime.Now };
            await _userFriendRepository.InsertAsync(userFriend);
            await _userFriendRepository.SaveChangesAsync();
        }
    }
}
