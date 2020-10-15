const urlApi = 'https://localhost:5001';

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
    GetSpecialPowers: () => {
        return fetch(`${urlApi}/api/SpecialPower`)
          .then(response => ApiClient.CatchError(response))
          .then(response => response.json())
    },
    GetSpecialPowerById: (id) => {
        return fetch(`${urlApi}/api/SpecialPower/${id}`)
          .then(response => ApiClient.CatchError(response))
          .then(response => response.json())
    },
    CreateSpecialPower: specialPower => {
        const payload = JSON.stringify(specialPower)
        return fetch(`${urlApi}/api/SpecialPower`, { method: 'POST', headers: { 'content-type': 'application/json' }, body: payload })
          .then(response => ApiClient.CatchError(response))
          .then(response => response.json());
    },
    UpdateSpecialPower: specialPower => {
        const payload = JSON.stringify(specialPower)
        return fetch(`${urlApi}/api/SpecialPower`, { method: 'PUT', headers: { 'content-type': 'application/json' }, body: payload })
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