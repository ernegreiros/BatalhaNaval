import React, { Component } from 'react';
import M from "materialize-css";


class Modal extends Component {

    componentDidMount() {
        const options = {
            onOpenStart: () => {
                console.log("Open Start");
            },
            onOpenEnd: () => {
                console.log("Open End");
            },
            onCloseStart: () => {
                console.log("Close Start");
            },
            onCloseEnd: () => {
                console.log("Close End");
            },
            inDuration: 250,
            outDuration: 250,
            opacity: 0.5,
            dismissible: false,
            startingTop: "4%",
            endingTop: "10%"
        };
        M.Modal.init(this.Modal, options);
    }

    render() {
        const { ModalBody, ModalId, ModalHeader } = this.props;

        return (

            <div
                ref={Modal => {
                    this.Modal = Modal;
                }}
                id={ModalId}
                className="modal"
            >
                <div className="modal-content">
                    <button className="right btn-floating modal-close btn red darken-4" id={`${ModalId}CloseButton`}>X</button>
                    <div className="section">
                        <div className="row container">
                            <h4 className="center" id={ModalId + 'Header'}><b>{ModalHeader}</b></h4>
                        </div>
                    </div>
                    <div className="divider"></div>
                    <div id={ModalId + 'Body'} style={{ margin: "1%" }}>
                        {ModalBody}
                    </div>
                </div>
            </div>
        );
    }
}

export default Modal;