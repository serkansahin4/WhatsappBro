using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFriendFeatures.GetUserFriends
{
    public class GetUserFriendsQueryHandler : IRequestHandler<GetUserFriendsQuery, List<UserFriendDetailDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserFriendRepository _userFriendRepository;

        public GetUserFriendsQueryHandler(IUserFriendRepository userFriendRepository)
        {
            _userFriendRepository = userFriendRepository;
        }

        public async Task<List<UserFriendDetailDto>> Handle(GetUserFriendsQuery request, CancellationToken cancellationToken)
        {
            List<UserFriendDetailDto> userFriendDetails = _mapper.Map<List<UserFriendDetailDto>>(await _userFriendRepository.GetsAsync(x => x.UserId == request.UserId));
            return userFriendDetails;
        }
    }
}
