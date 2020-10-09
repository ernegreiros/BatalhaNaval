import DataFormatter from '../Utils/DataFormatter';

const urlApi = 'http://localhost:5000/';

const ApiClient = {
    GetChampionships: () => {
        return fetch(`${urlApi}/api/championships`)
            .then(response => ApiClient.CatchError(response))
            .then(response => response.json());
    },
    AdminLogin: (credentials) => {
      return new Promise(res => setTimeout(() => res(), 3000))
        // return fetch(`${urlApi}/api/login`, { method: 'POST', headers: { 'content-type': 'application/json' }, body: credentials })
        //   .then(response => ApiClient.CatchError(response))
        //   .then(response => response.json());
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