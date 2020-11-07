import DataFormatter from './DataFormatter';
import M from "materialize-css";
import WebSocketHandler from '../Components/WebSocketHandler/WebSocketHandler';

const HelperModal = {
    ShowWantToConnectModal: (partnerPlayer, myCode) => {
        const helperModal = document.getElementById('HelperModal');
        const helperModalHeader = document.getElementById('HelperModalHeader');
        const helperModalBody = document.getElementById('HelperModalBody');

        const msg =
            `<h5 class="center">
                <p>O jogador <b>${partnerPlayer.name}</b> quer iniciar uma partida</p>
                <p>Você aceita?</p>
            </h5>
            <br />
            <button id="refuse" class="red btn-large">
                Recusar
            </button>
            <button id="accept" class="green btn-large right">
                Aceitar
            </button> 
            <br />`;

        helperModalHeader.innerHTML = `<b>Bora Jogar?!</b>`
        helperModalBody.innerHTML = msg;

        const instance = M.Modal.init(helperModal, {
            dismissible: false,
            onCloseStart: () => {
                helperModalBody.innerHTML = "";
                WebSocketHandler.ConnectionRefused(partnerPlayer.code);
            }
        });

        var refuseButton = document.getElementById("refuse");
        var acceptButton = document.getElementById("accept");
        
        refuseButton.onclick = () => {
            instance.close();
        }

        acceptButton.onclick = () => {
            WebSocketHandler.Connect(partnerPlayer.code, myCode);
        }

        instance.open();
    }
}

HelperModal.Refuse = () => {

}
export default HelperModal;