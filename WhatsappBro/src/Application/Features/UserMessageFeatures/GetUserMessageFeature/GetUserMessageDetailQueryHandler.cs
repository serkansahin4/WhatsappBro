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

namespace Application.Features.UserMessageFeatures.GetUserMessageFeature
{
    public class GetUserMessageDetailQueryHandler : IRequestHandler<GetUserMessageDetailQuery, List<UserMessageDto>>
    {
        private readonly IUserMessageRepository _userMessageRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetUserMessageDetailQueryHandler(IUserMessageRepository userMessageRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _userMessageRepository = userMessageRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserMessageDto>> Handle(GetUserMessageDetailQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _userManager.Users.SingleOrDefault(x => x.UserName == request.UserName).Id;
            List<UserMessageDto> userMessages = _mapper.Map<List<UserMessageDto>>(await _userMessageRepository.GetUserMessages(x => x.SenderId == userId || x.ReceiverId == userId));
            return userMessages;
        }
    }
}
