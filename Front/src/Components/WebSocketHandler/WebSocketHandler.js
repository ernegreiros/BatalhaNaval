import React from 'react';
import * as signalR from '@microsoft/signalr';
import PopUp from '../PopUp/PopUp';
import Modal from '../Modal/Modal';
import HelperModal from '../../Utils/HelperModal';

//let hubConnection = new signalR.HubConnectionBuilder().withUrl("/websocketHandler").build();  // usar quando rodar pela api
let hubConnection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5000/websocketHandler").build();  // usar quando rodar pelo front

const WebSocketHandler = async (userName) => {

    if (hubConnection.state === signalR.HubConnectionState.Disconnected) {
        await hubConnection.start();
    }

    hubConnection.invoke("RegisterCode", hubConnection.connectionId, userName).catch(function (err) {
        console.log(err.toString());
    });
}

WebSocketHandler.Connect = (partnerCode, mycode) => {

    hubConnection.invoke("Connect", hubConnection.connectionId, partnerCode, mycode).catch(function (err) {
        PopUp.showPopUp('error', 'Não foi possivel conectar ao jogador');
        console.log(err.toString());
    });
}

WebSocketHandler.AskForConnection = (partnerCode, mycode) => {
    hubConnection.invoke("AskForConnection", hubConnection.connectionId, partnerCode, mycode).catch(function (err) {
        PopUp.showPopUp('error', 'Não foi possivel conectar ao jogador');
        console.log(err.toString());
    });
}

WebSocketHandler.ConnectionRefused = (partnerCode) => {
    hubConnection.invoke("ConnectionRefused", partnerCode).catch(function (err) {    
        console.log(err.toString());
    });
}

WebSocketHandler.PlayerReady = (partnerCode,myName) => {
    hubConnection.invoke("PlayerReady", partnerCode, myName).catch(function (err) {    
        console.log(err.toString());
    });
}

hubConnection.on("AskingForConnection", function (player, myCode) {
    HelperModal.ShowWantToConnectModal(player, myCode);
});

hubConnection.on("Connected", function (matchId, player, partnerPlayer) {
    localStorage.setItem('match', JSON.stringify({"matchId": matchId, "adversary": partnerPlayer, "player":player }));
    localStorage.setItem('battle-field-theme', null)
    PopUp.showPopUp('success', 'Conectado');
    window.location.href = '/battlefield';
});

hubConnection.on("ConnectionRefused", function () {
    PopUp.showPopUp('error', 'Pedido de jogo recusado');
});

hubConnection.on("PlayerReady", function (partnerName) {
    PopUp.showPopUp('success', `${partnerName} terminou de posicionar os navios`);
});

export default WebSocketHandler;
