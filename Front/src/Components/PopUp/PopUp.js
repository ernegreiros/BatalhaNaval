import M from 'materialize-css';

const PopUp = {
    showPopUp: (type, message) => {
        
        if (type === 'success') {
            M.toast({ html: message, classes: 'green', displayLength: 3000 })
        }
        
        if (type === 'error') {
            M.toast({ html: message, classes: 'red', displayLength: 3000 })
        }
    }
}

export default PopUp;