import React, { Component } from 'react';
import LinkWrapper from '../../Utils/LinkWrapper';



class NavBar extends Component {
    render() {
        return (
            <div className="navbar-fixed">
                <nav>
                    <div className="nav-wrapper" >
                        <LinkWrapper to="/" className="brand-logo center" activeStyle={{}}>BATALHA NAVAL</LinkWrapper>
                        <ul id="nav-mobile" className="right">
                            <li><LinkWrapper to="/backoffice-login" activeStyle={{}} style={{marginRight:"2%"}}>BackOffice</LinkWrapper></li>
                        </ul>
                    </div>
                </nav>
            </div>
        );
    }
}
export default NavBar;