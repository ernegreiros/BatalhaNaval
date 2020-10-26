import UserService from "../Services/UserService";
import ShipsTypes from "../Enums/ShipsTypes";

const urlApi = 'https://localhost:5001';

const ApiClient = {
    GetChampionships: () => {
        return fetch(`${urlApi}/api/championships`)
            .then(response => ApiClient.CatchError(response))
            .then(response => response.json());
    },
    AdminLogin: (credentials) => {
        const payload = JSON.stringify(credentials);
        return fetch(`${urlApi}/api/Auth`, { method: 'POST', headers: { 'content-type': 'application/json' }, body: payload })
          .then(response => ApiClient.CatchError(response))
          .then(response => response.json())
          .then(response => UserService().saveToken(response.token))
    },
    GetSpecialPowers: () => {
      const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}` });
        return fetch(`${urlApi}/api/SpecialPower`, { headers })
          .then(response => ApiClient.CatchError(response))
          .then(response => response.json())
    },
    GetSpecialPowerById: (id) => {
        const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}` });
        return fetch(`${urlApi}/api/SpecialPower/${id}`, { headers })
          .then(response => ApiClient.CatchError(response))
          .then(response => response.json())
    },
    CreateSpecialPower: specialPower => {
        const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}`, 'content-type': 'application/json' });
        const payload = JSON.stringify(specialPower)
        return fetch(`${urlApi}/api/SpecialPower`, { method: 'POST', headers, body: payload })
          .then(response => ApiClient.CatchError(response))
          .then(response => response.json());
    },
    UpdateSpecialPower: specialPower => {
        const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}`, 'content-type': 'application/json' });
        const payload = JSON.stringify(specialPower)
        return fetch(`${urlApi}/api/SpecialPower`, { method: 'PUT', headers, body: payload })
          .then(response => ApiClient.CatchError(response))
          .then(response => response.json());
    },
    GetThemes: () => {
        const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}` });
        return fetch(`${urlApi}/api/Themes`, { headers })
          .then(response => ApiClient.CatchError(response))
          .then(response => response.json())
    },
    CreateTheme: theme => {
        const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}`, 'content-type': 'application/json' });
        const payload = JSON.stringify(theme)
        return fetch(`${urlApi}/api/Themes`, { method: 'POST', headers, body: payload })
          .then(response => ApiClient.CatchError(response))
          .then(response => response.json());
    },
    UpdateTheme: theme => {
        const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}`, 'content-type': 'application/json' });
        const payload = JSON.stringify(theme)
        return fetch(`${urlApi}/api/Themes`, { method: 'PUT', headers, body: payload })
          .then(response => ApiClient.CatchError(response))
          .then(response => response.json());
    },
    GetThemeShips: themeId => {
        return new Promise(resolve =>
          setTimeout(() => resolve({ ships: [ { id: 1, name: 'Navio Antigo 1 campo', type: ShipsTypes.OneField  } ] }), 3000))
        // const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}` });
        // return fetch(`${urlApi}/api/Ships`, { headers, params: { themeId } })
        //   .then(response => ApiClient.CatchError(response))
        //   .then(response => response.json())
    },
    CreateTeam: team => {
        return fetch(`${urlApi}/api/teams/create`, { method: 'POST', headers: { 'content-type': 'application/json' }, body: team })
            .then(response => response.json());
    },
    CatchError: response => {
        if (!response.ok) {
            throw Error(response.responseText);
        }
        return response;
    },
}

export default ApiClient;