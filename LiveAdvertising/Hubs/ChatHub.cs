using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveAdvertising.Models;
using LiveAdvertising.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace LiveAdvertising.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationContext context;

        public ChatHub(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task AddToGroup(string id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, id);
        }

        [Authorize]
        public async Task ShopSendMessage(string id, string message)
        {
            string hourMinutes = DateTime.Now.ToString("HH:mm");

            await Clients.OthersInGroup(id).SendAsync("ShopSendMessage", message, hourMinutes);

            Stream stream = await context.Streams.Where(x => x.Id == int.Parse(id)).FirstOrDefaultAsync();

            Message msg = new Message();

            msg.isAnswer = true;
            msg.Text = message;
            msg.Time = hourMinutes;
            msg.Stream = stream;
            msg.StreamId = stream.Id;

            await context.Messages.AddAsync(msg);

            await context.SaveChangesAsync();
        }

        public async Task UserSendMessage(string id, string message)
        {
            string hourMinutes = DateTime.Now.ToString("HH:mm");

            await Clients.OthersInGroup(id).SendAsync("UserSendMessage", message, hourMinutes);

            Stream stream = await context.Streams.Where(x => x.Id == int.Parse(id)).FirstOrDefaultAsync();

            Message msg = new Message();

            msg.isAnswer = false;
            msg.Text = message;
            msg.Time = hourMinutes;
            msg.Stream = stream;
            msg.StreamId = stream.Id;

            await context.Messages.AddAsync(msg);

            await context.SaveChangesAsync();
        }
    }
}
