import React from 'react'
import LinkWrapper from '../../Utils/LinkWrapper';

const InitialForm = ({ player }) => {
  return (
    <div className="row">
      <form className="col s12">
        <div className="row">
          <h3 className="center">Seja bem-vindo, <b>{player.name}</b>!</h3>
          <h4 className="center"><b>Seu Código: {player.code}</b></h4>
        </div>
        <br />
        <div className="row">
          <div className="input-field">
            <i className="material-icons prefix">contacts</i>
            <input id="contacts" placeholder="Código Segundo Jogador" type="tel" className="validate" />
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