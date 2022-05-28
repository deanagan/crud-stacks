using Microsoft.AspNetCore.SignalR;
using RealTimeChat.Service;

namespace RealTimeChat.Hubs
{
    public class AlertHub : Hub
    {
        private readonly string _botUser;
        private readonly IDictionary<string, UserConnection> _connections;

        public AlertHub(IDictionary<string, UserConnection> connections)
        {
            _botUser = "Chat Bot";
            _connections = connections;
        }

        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room ?? "Default");

            _connections[Context.ConnectionId] = userConnection;

            await Clients.Group(userConnection.Room ?? "Default").SendAsync("ReceiveMessage", _botUser, $"{userConnection.Name} has joined {userConnection.Room}");
        }

        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
            {
                if (userConnection.Room != null)
                {
                    await Clients.Group(userConnection.Room)
                        .SendAsync("ReceiveMessage", userConnection.Name, message);
                }
            }
        }
    }
}
