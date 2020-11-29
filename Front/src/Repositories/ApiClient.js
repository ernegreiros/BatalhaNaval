import UserService from "../Services/UserService";

const urlApi = 'https://localhost:5001';

const ApiClient = {
  GetChampionships: () => {
    return fetch(`${urlApi}/api/championships`)
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json());
  },
  Login: (credentials) => {
    const payload = JSON.stringify(credentials);
    return fetch(`${urlApi}/api/Auth`, { method: 'POST', headers: { 'content-type': 'application/json' }, body: payload })
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json())
      .then(response => UserService().saveToken(response.token))
  },
  GetPlayer: () => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}` });
    return fetch(`${urlApi}/api/Player`, { headers })
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json())
  },
  CreatePlayer: singUpInfo => {
    const payload = JSON.stringify(singUpInfo);
    const headers = new Headers({ 'content-type': 'application/json' });
    return fetch(`${urlApi}/api/Player`, { method: 'POST', headers, body: payload })
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json())
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
  DeleteSpecialPower: specialPower => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}`, 'content-type': 'application/json' });
    return fetch(`${urlApi}/api/SpecialPower/${specialPower}`, { method: 'DELETE', headers })
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
  DeleteTheme: themeId => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}`, 'content-type': 'application/json' });
    return fetch(`${urlApi}/api/Themes/${themeId}`, { method: 'DELETE', headers })
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json());
  },
  GetThemeShips: themeId => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}` });
    return fetch(`${urlApi}/api/Ships/ByTheme/${themeId}`, { headers })
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json())
  },
  CreateShip: ship => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}`, 'content-type': 'application/json' });
    const payload = JSON.stringify(ship)
    return fetch(`${urlApi}/api/Ships`, { method: 'POST', headers, body: payload })
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json());
  },
  UpdateShip: ship => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}`, 'content-type': 'application/json' });
    const payload = JSON.stringify(ship)
    return fetch(`${urlApi}/api/Ships`, { method: 'PUT', headers, body: payload })
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json());
  },
  DeleteShip: shipId => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}`, 'content-type': 'application/json' });
    return fetch(`${urlApi}/api/Ships/${shipId}`, { method: 'DELETE', headers })
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json());
  },
  GetCurrentMatch: () => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}` });
    return new Promise((res, rej) => {
      fetch(`${urlApi}/api/Match`, { headers })
        .then(response => ApiClient.CatchError(response))
        .then(async response => {
          const { match = {} } = await response.json()
          res({ ...match, player1Ready: match.player1Ready === 1, player2Ready: match.player2Ready === 1 })
        })
        .catch(e => rej(e))
    });
  },
  GetPositions: () => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}` });
    return fetch(`${urlApi}/api/BattleField`, { headers })
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json())
  },
  GetPositionsByPlayerId: (playerId) => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}` });
    return fetch(`${urlApi}/api/BattleField/${playerId}`, { headers })
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json())
  },
  RegisterPositions: positions => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}`, 'content-type': 'application/json' });
    const payload = JSON.stringify(positions)
    return fetch(`${urlApi}/api/BattleField/positions`, { method: 'POST', headers, body: payload })
      .then(response => ApiClient.CatchError(response))
      .then(response => response.json());
  },
  AttackPositions: positions => {
    const headers = new Headers({ 'Authorization': `Bearer ${UserService().getToken()}`, 'content-type': 'application/json' });
    const payload = JSON.stringify(positions)
    return fetch(`${urlApi}/api/BattleField/attack`, { method: 'POST', headers, body: payload })
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