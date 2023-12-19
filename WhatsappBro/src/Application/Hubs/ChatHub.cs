using Application.DTOs;
using Application.Features.UserFriendFeatures.GetUserFriendDetails;
using Application.Features.UserMessageFeatures;
using Application.Features.UserMessageFeatures.GetUserMessageFeature;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hubs
{
    public class ChatHub : Hub
    {
        readonly IHttpContextAccessor _httpContextAncessor;
        readonly IMediator _mediator;
        readonly UserManager<AppUser> _userManager;
        public ChatHub(IHttpContextAccessor httpContextAncessor, IMediator mediator, UserManager<AppUser> userManager)
        {
            _httpContextAncessor = httpContextAncessor;
            _mediator = mediator;
            _userManager = userManager;
        }


        public override async Task OnConnectedAsync()
        {
            string connectedUserName = _httpContextAncessor.HttpContext.User.Identity.Name; //Giriş yapan kullanıcının
            string connectionId = Context.ConnectionId; //Giriş yapan kullanıcının

            if (!ChatHubClient.chatClients.Any(x => x.UserName == connectedUserName))
            {
                ChatHubClient.chatClients.Add(new ChatClient
                {
                    ConnectionId = connectionId,
                    UserName = connectedUserName
                });
            }
            else
            {
                ChatClient client = ChatHubClient.chatClients.SingleOrDefault(x => x.UserName == connectedUserName);
                client.ConnectionId = connectionId;
            }




            //Guid connectedUserId = _userManager.Users.SingleOrDefault(x => x.UserName == connectedUserName).Id;

            //chatClients.Add(new ChatClient
            //{
            //    ConnectionId = Context.ConnectionId,
            //    UserName = connectedUserName
            //});

            //List<UserFriendDetailDto> userFriends = await _mediator.Send(new GetUserFriendDetailQuery
            //{
            //    UserName = connectedUserName
            //});

            //List<UserMessageDto> userMessages = await _mediator.Send(new GetUserMessageDetailQuery
            //{
            //    UserName = connectedUserName
            //});

            //List<ChatClient> connectionFriends = chatClients.Where(x =>
            //      userFriends.Any(y => x.UserName == y.User.UserName) ||
            //      userFriends.Any(y => x.UserName == y.Friend.UserName)).ToList();

            //List<ChatClient> onlyFriends = userFriends.Where(x => x.User.UserName == connectedUserName).ToList()
            //    .Select(x => new ChatClient
            //    {
            //        ConnectionId = chatClients.SingleOrDefault(y => y.UserName == x.Friend.UserName)?.ConnectionId,
            //        UserName = x.Friend.UserName
            //    }).ToList();

            //List<ChatClientMessage> chatClientMessages = new List<ChatClientMessage>();
            //foreach (ChatClient friend in onlyFriends)
            //{
            //    string message = userMessages.OrderByDescending(x => x.CreatedDate).First(x => x.SenderUser.UserName == friend.UserName).Message;
            //    chatClientMessages.Add(new ChatClientMessage
            //    {
            //        ConnectionId = friend.ConnectionId,
            //        Message = message.Substring(0, 30),
            //        UserName = friend.UserName,
            //    });
            //}

            //await Clients.Caller.SendAsync("notificationMessage", Context.ConnectionId, chatClientMessages);

            base.OnConnectedAsync();
        }

    }
}
