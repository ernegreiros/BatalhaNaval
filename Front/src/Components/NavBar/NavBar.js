import React, { Component } from 'react';
import LinkWrapper from '../../Utils/LinkWrapper';


class NavBar extends Component {
    render() {
        return (
            <div className="navbar-fixed">
                <nav>
                    <div className="nav-wrapper" >
                        <LinkWrapper to="/" className="brand-logo center" activeStyle={{}}>BATALHA NAVAL</LinkWrapper>
                    </div>
                </nav>
            </div>
        );
    }
}
export default NavBar;