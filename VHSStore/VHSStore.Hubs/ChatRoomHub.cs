using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHSStore.Api.Hubs
{
    public class ChatRoomHub : Hub<IChatRoomHub>
    {
        private const string GeneralRoom = "GeneralRoom";
        private const string HorrorRoom = "HorrorRoom";
        private const string ComedyRoom = "ComedyRoom";
        private List<string> Rooms = new List<string>() { GeneralRoom, HorrorRoom, ComedyRoom};

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(this.Context.ConnectionId, GeneralRoom);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(this.Context.ConnectionId, GeneralRoom);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task ConnectToRoom(string room)
        {
            try
            {
                if (Rooms.Contains(room))
                {
                    await Groups.AddToGroupAsync(this.Context.ConnectionId, room);

                    await Clients.Caller.GroupConnectionResponse(new ConnectionStatus() { ConnectionResponse = "Successfully Connected.", GroupName = room});
                }
                else
                {
                    await Clients.Caller.GroupConnectionResponse(new ConnectionStatus() { ConnectionResponse = "Failed Connection, Room does not Exist.", GroupName = room});
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.GroupConnectionResponse(new ConnectionStatus() { ConnectionResponse = $"Failed Connection. Error: {ex.Message}", GroupName = room});
            }
        }

        public async Task DisconnectFromRoom(string room)
        {
            try
            {
                if (Rooms.Contains(room))
                {
                    await Groups.RemoveFromGroupAsync(this.Context.ConnectionId, room);
                    await Clients.Caller.GroupDisconnectionResponse(new ConnectionStatus() { ConnectionResponse = "Successfully Disconnected.", GroupName = room});
                }
                else
                {
                    await Clients.Caller.GroupDisconnectionResponse(new ConnectionStatus() { ConnectionResponse = "Failed Disconnect, Room does not Exist.", GroupName = room});
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.GroupDisconnectionResponse(new ConnectionStatus() { ConnectionResponse = $"Failed Disconnect. Error: {ex.Message}", GroupName = room});
            }
        }

        public async Task SendMessageToRoom(string message, string user, string room)
        {
            try
            {
                if (Rooms.Contains(room))
                {
                    await Clients.Group(room).SendMessage(new ChatRoomMessage() { Message = message, User = user });
                    await Clients.Caller.SendingMessageResponse("Successfully Sent Message.");
                }
                else
                {
                    await Clients.Caller.SendingMessageResponse("Failed To Send Message, Room Does Not Exist.");
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendingMessageResponse($"Failed To Send Message. Error: {ex.Message}");
            }
        }
    }
}
