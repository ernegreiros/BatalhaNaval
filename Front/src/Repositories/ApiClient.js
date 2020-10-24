import UserService from "../Services/UserService";

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