using System;
using System.Threading.Tasks;

namespace VHSStore.Api.Hubs
{
    public interface IChatRoomHub
    {
        Task GroupConnectionResponse(ConnectionStatus connectionStatus);

        Task GroupDisconnectionResponse(ConnectionStatus connectionStatus);

        Task SendingMessageResponse(string messageResponse);

        Task SendMessage(ChatRoomMessage messageData);
    }

    public class ConnectionStatus
    {
        public string ConnectionResponse { get; set; }
        public string GroupName { get; set; }
    }

    public class ChatRoomMessage
    {
        public string Message { get; set; }
        public string User { get; set; }
        public DateTime MessageDate { get; protected set; }

        public ChatRoomMessage()
        {
            MessageDate = DateTime.Now;
        }
    }
}