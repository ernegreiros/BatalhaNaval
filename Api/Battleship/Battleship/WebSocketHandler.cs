﻿using BattleshipApi.Match.DML;
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

        public async Task Connect(string myConnectionId, string partnerCode, string myCode)
        {
            var player = playerObject.FindPlayerByCode(myCode);
            var partnerPlayer = playerObject.FindPlayerByCode(partnerCode);

            if (!BondAlreadyExists(partnerCode, myCode))
                connections.PlayersBinded.Add(new PlayerBinded(myCode, partnerCode));

            var partnerConnectionId = GetConnectionId(partnerCode);

            var currentMatchPlayer = matchObject.CurrentMatch(player.Login);
            var currentMatchPartner = matchObject.CurrentMatch(partnerPlayer.Login);

            var matchId = currentMatchPartner?.ID;

            if (currentMatchPlayer is null || currentMatchPartner is null || !PlayersAlreadyInSameMatch(currentMatchPlayer, currentMatchPartner))
            {
                matchId = matchObject.CreateMatch(new Match()
                {
                    Player1 = player.ID,
                    Player2 = partnerPlayer.ID,
                    CurrentPlayer = partnerPlayer.ID 
                });
            }

            ChangePlayerReady(partnerCode, isReady: false);
            ChangePlayerReady(myCode, isReady: false);

            await Clients.Caller.SendAsync("Connected", matchId, player, partnerPlayer);
            await Clients.Client(partnerConnectionId).SendAsync("Connected", matchId, partnerPlayer, player);
        }



        public async Task AskForConnection(string myConnectionId, string partnerCode, string myCode)
        {
            var player = playerObject.FindPlayerByCode(myCode);
            var partnerPlayer = playerObject.FindPlayerByCode(partnerCode);

            var partnerConnectionId = GetConnectionId(partnerCode);

            await Clients.Client(partnerConnectionId).SendAsync("AskingForConnection", player, partnerCode);
        }

        public async Task ConnectionRefused(string partnerCode)
        {
            var partnerConnectionId = GetConnectionId(partnerCode);
            await Clients.Client(partnerConnectionId).SendAsync("ConnectionRefused");
        }

        public async Task PlayerReady(string partnerCode, string myName, string myCode, string ships)
        {
            var player = playerObject.FindPlayerByCode(myCode);
            var currentPlayerMatch = matchObject.CurrentMatch(player.Login);
            var partnerConnectionId = GetConnectionId(partnerCode);

            ChangePlayerReady(myCode, isReady: true);

            await Clients.Client(partnerConnectionId).SendAsync("PlayerReady", myName, ships);

            if (player.ID == currentPlayerMatch.Player1)
            {
                currentPlayerMatch.Player1Ready = 1;
            } 
            else
            {
                currentPlayerMatch.Player2Ready = 1;
            }

            if (AllPlayersReady(partnerCode, myCode))
            {
                currentPlayerMatch.Status = BattleshipApi.Match.DML.Enumerados.MatchStatus.AllPlayersReady;
            }

            matchObject.UpdateMatch(currentPlayerMatch);

            if (AllPlayersReady(partnerCode, myCode))
            {
                await Clients.Caller.SendAsync("StartGame", currentPlayerMatch.CurrentPlayer);
                await Clients.Client(partnerConnectionId).SendAsync("StartGame", currentPlayerMatch.CurrentPlayer);
            }
        }

        private bool AllPlayersReady(string partnerCode, string myCode)
        {
            return connections.Connections.FirstOrDefault(c => c.Code == myCode).Ready && connections.Connections.FirstOrDefault(c => c.Code == partnerCode).Ready;
        }

        public async Task Action(string adversaryCode, string action, int x, int y, object specialPowerPositions, bool hitTarget, int? winner)
        {
            var connectionId = GetConnectionId(adversaryCode);
            await Clients.Client(connectionId).SendAsync(action, x, y, specialPowerPositions, hitTarget, winner);
        }

        private bool BondAlreadyExists(string partnerCode, string myCode)
        {
            return connections.PlayersBinded.Any(x => (x.Code1 == myCode && x.Code2 == partnerCode)
                                                   || (x.Code2 == myCode && x.Code1 == partnerCode));
        }

        private bool PlayersAlreadyInSameMatch(Match currentMatchPlayer, Match currentMatchPartner)
        {
            bool inSameMatch = currentMatchPlayer.ID == currentMatchPartner.ID;
            return inSameMatch;
        }

        private bool ConnectionAlreadyExists(string code)
        {
            return connections.Connections.Any(x => x.Code == code);
        }

        private void AddOrUpdateConnectionId(string code, string connectionId)
        {
            if (ConnectionAlreadyExists(code))
            {
                var item = connections.Connections.FirstOrDefault(c => c.Code == code);
                item.ConnectionId = connectionId;
                return;
            }

            connections.Connections.Add(new Connection(code, connectionId, false));
        }

        private string GetConnectionId(string code)
        {
            return connections.Connections.FirstOrDefault(c => c.Code == code).ConnectionId;
        }

        private void ChangePlayerReady(string code, bool isReady)
        {
            var playerConnection = connections.Connections.FirstOrDefault(c => c.Code == code);
            playerConnection.Ready = isReady;
        }
        private void ChangeMatchPlayerReady()
        {
        }
    }
}
