using BattleshipApi.Match.DML;
using BattleshipApi.Match.DML.Interfaces;
using BattleshipApi.Models.Player.Out;
using BattleshipApi.Player.BLL;
using BattleshipApi.Player.DAL;
using BattleshipApi.Player.DML.Interfaces;
using DataBaseHelper;
using DataBaseHelper.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship
{
    public class WebSocketHandler : Hub
    {
        private readonly WebSocketConnections connections;
        private readonly IUnitOfWork IUnitOfWork;
        private readonly IBoPlayer playerObject;
        private readonly IBoMatch matchObject;

        public WebSocketHandler(WebSocketConnections connections, IUnitOfWork unitOfWork, IBoMatch match, IBoPlayer player)
        {
            this.connections = connections;
            IUnitOfWork = unitOfWork;
            playerObject = player;
            matchObject = match;
        }

        public async Task RegisterCode(string connectionId, string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return;

            var player = playerObject.FindPlayerByUserName(userName);

            if (player is null)
                return;

            AddOrUpdateConnectionId(player.Code, connectionId);

            await Clients.Caller.SendAsync("CodeRegistered", player.Code);
        }

        private void AddOrUpdateConnectionId(string code, string connectionId)
        {
            if (ConnectionAlreadyExists(code))
            {
                connections.Connections[code] = connectionId;
                return;
            }

            connections.Connections.Add(code, connectionId);
        }

        public async Task Connect(string myConnectionId, string partnerCode, string myCode)
        {
            var player = playerObject.FindPlayerByCode(myCode);
            var partnerPlayer = playerObject.FindPlayerByCode(partnerCode);

            if (!BondAlreadyExists(partnerCode, myCode))
                connections.PlayersBinded.Add(new PlayerBinded(myCode, partnerCode));

            var partnerConnectionId = connections.Connections.FirstOrDefault(c => c.Key == partnerCode).Value;

            //adicionar tratamento para não quebrar quando existe partida
            var matchId = matchObject.CreateMatch(new Match()
            {
                Player1 = player.ID,
                Player2 = partnerPlayer.ID
            });

            await Clients.Caller.SendAsync("Connected", matchId);
            await Clients.Client(partnerConnectionId).SendAsync("Connected", matchId);

        }

        public async Task AskForConnection(string myConnectionId, string partnerCode, string myCode)
        {
            var player = playerObject.FindPlayerByCode(myCode);
            var partnerPlayer = playerObject.FindPlayerByCode(partnerCode);

            var partnerConnectionId = connections.Connections.FirstOrDefault(c => c.Key == partnerCode).Value;

            await Clients.Client(partnerConnectionId).SendAsync("AskingForConnection", player, partnerCode);
        }

        public async Task ConnectionRefused(string partnerCode)
        {
            var partnerConnectionId = connections.Connections.FirstOrDefault(c => c.Key == partnerCode).Value;
            await Clients.Client(partnerConnectionId).SendAsync("ConnectionRefused");
        }

        public async Task Action(string mycode, string action, string x, string y)
        {
            var connectionId = connections.Connections.Where(c => c.Key == mycode).FirstOrDefault().Value;
            await Clients.Client(connectionId).SendAsync(action, x, y);
        }

        private bool ConnectionAlreadyExists(string code)
        {
            return connections.Connections.Any(x => x.Key == code);
        }

        private bool BondAlreadyExists(string partnerCode, string myCode)
        {
            return connections.PlayersBinded.Any(x => (x.Code1 == myCode && x.Code2 == partnerCode)
                                                   || (x.Code2 == myCode && x.Code1 == partnerCode));
        }

    }
}
