using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LiveAdvertising.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            await Clients.Caller.SendAsync("Connected");
        }
    }
}
