using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Helpers.FileHelper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.GetUserFriendsFeatures
{
    public class GetUserFriendsQueryHandler : IRequestHandler<GetUserFriendsQuery, List<UserDetailDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        readonly IMapper _mapper;
        private readonly IUserFriendRepository _userFriendRepository;
        public GetUserFriendsQueryHandler(UserManager<AppUser> userManager, IMapper mapper, IUserFriendRepository userFriendRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userFriendRepository = userFriendRepository;
        }
        public async Task<List<UserDetailDto>> Handle(GetUserFriendsQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _userManager.Users.SingleOrDefault(x => x.UserName == request.UserName).Id;
            List<UserFriend> userFriends = await _userFriendRepository.GetUserFriends(x => x.UserId == userId || x.FriendId == userId);

            List<UserDetailDto> userDetailDtos = userFriends.Select(x =>
            {
                var userDetailDto = new UserDetailDto
                {
                    Id = x.UserId == userId ? x.FriendId : x.UserId,
                    ThumbnailPath = x.UserId == userId ? x.Friend.ThumbnailPath : x.User.ThumbnailPath,
                    UserName = x.UserId == userId ? x.Friend.UserName : x.User.UserName,
                    ThumbnailBase64 = FileHelper.ConvertToBase64(Path.Combine(request.RootPath, x.UserId==userId?x.Friend.ThumbnailPath:x.User.ThumbnailPath))
                };
                return userDetailDto;
            }).ToList();
            return userDetailDtos;
        }
    }
}
