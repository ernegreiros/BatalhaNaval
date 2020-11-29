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

    return hubConnection;
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

    return hubConnection;
}

WebSocketHandler.ConnectionRefused = (partnerCode) => {
    hubConnection.invoke("ConnectionRefused", partnerCode).catch(function (err) {
        console.log(err.toString());
    });
}

WebSocketHandler.PlayerReady = (partnerCode, myName, myCode, ships) => {
    hubConnection.invoke("PlayerReady", partnerCode, myName, myCode, ships).catch(function (err) {
        console.log(err.toString());
    });
}

WebSocketHandler.TakeShot = (mycode, action, x, y, hitTarget) => {
    hubConnection.invoke("Action", mycode, action, x, y, hitTarget).catch(function (err) {
        console.log(err.toString());
    });
}
export default WebSocketHandler;
