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

namespace Application.Features.UserFeatures.GetUsersFeatures
{
    public class GetSearchUsersQueryHandler : IRequestHandler<GetSearchUsersQuery, List<UserDetailDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        readonly IMapper _mapper;
        private readonly IUserFriendRepository _userFriendRepository;
        public GetSearchUsersQueryHandler(UserManager<AppUser> userManager, IMapper mapper, IUserFriendRepository userFriendRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userFriendRepository = userFriendRepository;
        }

        public async Task<List<UserDetailDto>> Handle(GetSearchUsersQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _userManager.Users.SingleOrDefault(x => x.UserName == request.UserName).Id;
            List<UserFriend> userFriends = await _userFriendRepository.GetUserFriends(x => x.UserId == userId || x.FriendId == userId);

            List<AppUser> appUsers = _userManager.Users
                .Where(x => x.UserName != request.UserName && x.UserName.ToLower().Contains(request.SearchText.ToLower()))
                .OrderBy(x => x.UserName).Take(5).ToList()
                .Where(x=>!userFriends
                .Any(y => y.User.UserName == x.UserName || y.Friend.UserName == x.UserName)).ToList(); 


            List<UserDetailDto> userDetailDtos = _mapper.Map<List<UserDetailDto>>(appUsers);
            userDetailDtos = userDetailDtos.ConvertAll(x =>
            {
                x.ThumbnailBase64 = FileHelper.ConvertToBase64(Path.Combine(request.RootPath, x.ThumbnailPath));
                return x;
            });
            return userDetailDtos;
        }
    }
}
