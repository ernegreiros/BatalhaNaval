import React from 'react'
import LinkWrapper from '../../Utils/LinkWrapper';

const InitialForm = () => {
    return (
        <div className="row">
            <form className="col s12">
                <div className="row">
                    <h3 className="center"><b>Seu Código: 123456 </b></h3>
                </div>
                <br />
                <div className="row">
                    <div className="input-field">
                        <i className="material-icons prefix">contacts</i>
                        <input id="contacts" type="tel" className="validate" />
                        <label htmlFor="contacts">Código Segundo Jogador</label>
                    </div>
                </div>
                <div className="row">
                    <LinkWrapper to="/battleField" className="waves-effect waves-light darkbg btn-large right" activeStyle={{}}>
                        <i className="material-icons right">play_circle_outline</i>
                        Jogar
                    </LinkWrapper>
                </div>
            </form>
        </div>
    );
}

export default InitialForm;