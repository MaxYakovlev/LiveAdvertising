using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LiveAdvertising.Hubs
{
    public class ChatHub : Hub
    {
        public async Task AddToGroup(string id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, id);
        }

        public async Task ShopSendMessage(string id, string message)
        {
            string hourMinutes = DateTime.Now.ToString("HH:mm");

            await Clients.OthersInGroup(id).SendAsync("ShopSendMessage", message, hourMinutes);
        }

        public async Task UserSendMessage(string id, string message)
        {
            string hourMinutes = DateTime.Now.ToString("HH:mm");

            await Clients.OthersInGroup(id).SendAsync("UserSendMessage", message, hourMinutes);
        }
    }
}
