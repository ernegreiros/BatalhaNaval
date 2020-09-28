using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship
{
    public class WebSocketHandler : Hub
    {
        private readonly WebSocketConnections connections;

        public WebSocketHandler(WebSocketConnections connections)
        {
            this.connections = connections;
        }

        public async Task RegisterCode(string connectionId)
        {
            var code = Helper.KeyGenerator();
            connections.Connections.Add(code, connectionId);
            await Clients.Caller.SendAsync("CodeRegistered", code);
        }

        public async Task Connect(string myCode, string partnerCode)
        {
            // To do, alem de criar conexão, salvar isso na tabela de partidas depois
            connections.Binds.Add(new Bind(myCode, partnerCode));

            var partnerConnectionId = connections.Connections.FirstOrDefault(c => c.Key == partnerCode).Value;

            await Clients.Caller.SendAsync("Connected");
            await Clients.Client(partnerConnectionId).SendAsync("Connected");
        }

        public async Task Action(string mycode, string action, string x, string y)
        {
           var connectionId = connections.Connections.Where(c => c.Key == mycode).FirstOrDefault().Value;
           await Clients.Client(connectionId).SendAsync(action, x, y);
        }
    }
}
