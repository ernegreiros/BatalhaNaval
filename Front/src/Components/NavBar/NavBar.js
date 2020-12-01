import React, { Component, Fragment } from 'react';
import UserService from '../../Services/UserService';
import LinkWrapper from '../../Utils/LinkWrapper';
import Modal from '../Modal/Modal';
import PaymentForm from '../PaymentForm/PaymentForm';
import M from "materialize-css";


class NavBar extends Component {

    OpenModal = () => {
        const storeModal = document.getElementById('Store');

        var instance = M.Modal.init(storeModal, {
            dismissible: false
        });

        instance.open();
    }

    render() {
        return (
            <Fragment>
                <div className="navbar-fixed">
                    <nav>
                        <div className="nav-wrapper" >
                            <LinkWrapper to="/home" className="brand-logo center" activeStyle={{}}>BATALHA NAVAL</LinkWrapper>
                            <ul id="nav-mobile" className="right">
                                <li onClick={() => { this.OpenModal() }}><i className="material-icons hoverable" style={{ marginRight: "5%" }} >shopping_cart</i></li>
                                <li><LinkWrapper to="/backoffice-login" activeStyle={{}} style={{ marginRight: "2%" }}>BackOffice</LinkWrapper></li>
                                <li onClick={() => { UserService().saveToken(null) }}><LinkWrapper to="/" activeStyle={{}} style={{ marginRight: "2%" }} >Logoff</LinkWrapper></li>
                            </ul>
                        </div>
                    </nav>
                </div>
                <Modal
                    ModalId="Store"
                    ModalHeader="Ficando sem BN Points!?"
                    ModalBody={
                        <div className="row" >

                            <div className="row" style={{ marginRight: "4%" }}>
                                <p>BN Points são usados durante a partida para comprar os poderes especiais, você não vai ficar sem BN Points não é? Bora comprar alguns na loja</p>
                            </div>

                            <div className="row" style={{ marginRight: "4%" }}>
                                <PaymentForm />
                            </div>

                        </div>
                    }
                />
            </Fragment>
        );
    }
}
export default NavBar;