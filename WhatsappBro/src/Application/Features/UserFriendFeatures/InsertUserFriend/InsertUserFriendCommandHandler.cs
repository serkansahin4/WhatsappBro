using Application.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
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
        public async Task Handle(InsertUserFriendCommand request, CancellationToken cancellationToken)
        {
            UserFriend? userFriend = await _userFriendRepository.GetAsync(x => (x.UserId == request.UserId && x.FriendId == request.FriendId) || x.UserId == request.FriendId && x.FriendId == request.UserId);
            if (userFriend is not null)
            {
                throw new AlreadyFriendException(ExceptionMessage.UserFriend.AlreadyFriend);
            }

            userFriend = new UserFriend { UserId = request.UserId, FriendId = request.FriendId };
            await _userFriendRepository.InsertAsync(userFriend);
        }
    }
}
