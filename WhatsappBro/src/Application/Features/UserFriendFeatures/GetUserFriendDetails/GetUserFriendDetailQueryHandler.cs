using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFriendFeatures.GetUserFriendDetails
{
    public class GetUserFriendDetailQueryHandler : IRequestHandler<GetUserFriendDetailQuery, List<UserFriendDetailDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserFriendRepository _userFriendRepository;
        readonly UserManager<AppUser> _userManager;

        public GetUserFriendDetailQueryHandler(IUserFriendRepository userFriendRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userFriendRepository = userFriendRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<UserFriendDetailDto>> Handle(GetUserFriendDetailQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _userManager.Users.SingleOrDefault(x => x.UserName == request.UserName).Id;
            List<UserFriendDetailDto> userFriendDetails = _mapper.Map<List<UserFriendDetailDto>>(await _userFriendRepository.GetUserFriends(x => x.UserId == userId || x.FriendId == userId));
            return userFriendDetails;
        }
    }
}
