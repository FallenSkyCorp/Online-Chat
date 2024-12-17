using Chat.Core.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MutniyZadanieChatASPNet.API.Hubs
{
    public interface IChatClient
    {
        public Task ReceiveMessage(string username, string messsage);
    }

    public class ChatHub : Hub<IChatClient>
    {
        private IDistributedCache _cache;

        private static List<string> _usernames = new List<string>();

        public ChatHub(IDistributedCache cache)
        {
            _cache = cache;
        }

        public bool IsContainsUsername(string username)
        {
            if (_usernames.Contains(username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task JoinChat(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

            _usernames.Add(connection.Username);

            var connectionString = JsonSerializer.Serialize(connection);

            await _cache.SetStringAsync(Context.ConnectionId, connectionString);

            await Clients
                .OthersInGroup(connection.ChatRoom)
                .ReceiveMessage("Admin", $"{connection.Username} присоединился к чату.");
        }

        public async Task SendMessage(string messageString)
        {
            var connectionString = await _cache.GetAsync(Context.ConnectionId);

            var connection = JsonSerializer.Deserialize<UserConnection>(connectionString);

            if (connection is not null)
            {
                await Clients
                .OthersInGroup(connection.ChatRoom)
                .ReceiveMessage(connection.Username, messageString);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionString = await _cache.GetAsync(Context.ConnectionId);

            if (connectionString == null)
            {
                await base.OnDisconnectedAsync(exception);

                return;
            }

            var connection = JsonSerializer.Deserialize<UserConnection>(connectionString);

            if (connection is not null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatRoom);

                await _cache.RemoveAsync(Context.ConnectionId);

                _usernames.Remove(connection.Username);

                await Clients
                    .Group(connection.ChatRoom)
                    .ReceiveMessage("Admin", $"{connection.Username} вышел из чата.");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}

