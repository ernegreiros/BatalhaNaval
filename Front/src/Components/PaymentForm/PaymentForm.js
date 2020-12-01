import React, { Component, Fragment } from 'react'
import creditCard from '../../Images/creditCard.png';

const PaymentForm = () => {

    let price = [25,50,70];
    return (
        <Fragment>
            <div className="row" style={{ marginLeft: "5%" }}>
                <form className="col l6">
                    <div className="row">
                        <div className="input-field col s6">
                            <button onClick={()=>{}} className="darkbg btn-large" style={{ width:"100%" }}>
                                600 BN
                            </button>
                            <p>{price[1].toLocaleString('pt-br', {style: 'currency', currency: 'BRL'})}</p>
                        </div>
                        <div className="input-field col s6">
                            <button onClick={()=>{}} className="darkbg btn-large" >
                                900 BN
                            </button>
                            <p>{price[2].toLocaleString('pt-br', {style: 'currency', currency: 'BRL'})}</p>
                        </div>
                    </div>
                    <div className="row" style={{ marginTop: "-50%" }}>
                        <div className="input-field col s12">
                            <input value="Número do cartão" type="text" className="validate" />
                        </div>
                    </div>
                    <div className="row" style={{ marginTop: "-50%" }}>
                        <div className="input-field col s6">
                            <input placeholder="Validade" id="first_name" type="text" className="validate" />
                        </div>
                        <div className="input-field col s6">
                            <input placeholder="CVV" type="text" className="validate" />
                        </div>
                    </div>
                    <div className="row" style={{ marginTop: "-50%" }}>
                        <div className="input-field col s12">
                            <input value="Nome Completo" type="text" className="validate" />
                        </div>
                    </div>
                    <div className="row" style={{ marginTop: "-50%" }}>
                        <div className="input-field col s12">
                            <input value="CPF" type="text" className="validate" />
                        </div>
                    </div>
                </form>
                <div className="col l6">
                    <img src={creditCard}></img>
                    <div className="row" style={{ marginLeft: "5%" }}>
                        <p>Quantidade a comprar: </p>
                    </div>
                    <div className="row" style={{ marginLeft: "5%" }}>
                        <button onClick={()=>{}} className="darkbg btn-large center" style={{ width:"100%" }}>
                            Comprar
                        </button>
                    </div>
                </div>
            </div>
        </Fragment>
    );
}

export default PaymentForm;