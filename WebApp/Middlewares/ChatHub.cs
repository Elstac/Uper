using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Data.Repositories;
using WebApp.Models;

namespace WebApp.Middlewares
{
    public interface IChatHub
    {
        Task SendMessageToGroup(string groupName, string message, string userName);
        Task AddToGroup(string groupName);
        Task RemoveFromGroup(string groupName);
    }

    public class ChatHub : Hub,IChatHub
    {
        private IChatEntryRepository chatEntryRepository;
        private IAccountManager accountManager;
        public ChatHub(IChatEntryRepository chatEntryRepository, IAccountManager accountManager)
        {
            this.chatEntryRepository = chatEntryRepository;
            this.accountManager = accountManager;
    }
        public Task SendMessageToGroup(string groupName, string message, string userName)
        {
            //To Do 
            //Add username
            chatEntryRepository.Add(new ChatEntry { Message = message, TripId = Int32.Parse(groupName), UserName = userName });
            return Clients.Group(groupName).SendAsync("Send", $"{userName}: {message}");
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

           // await Clients.Group(groupName).SendAsync("Send", $"{userName} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }

    }
}
