import React from 'react';

const ParallaxImage = (props) => {
    const imagePath = props.imagePath;
    return (
        <div className={`parallax-container`} style={props.style}>
            <div className="parallax"><img className="responsive-img" alt="" src={imagePath} /></div>
        </div>
    );
}

export default ParallaxImage;