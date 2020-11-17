export default function UserService() {
  function saveToken(token) {
    localStorage.setItem('user-token', token);
  }

  function getToken() {
    return localStorage.getItem('user-token');
  }

  function setPlayerData(data) {
    localStorage.setItem('player', JSON.stringify(data));
  }

  function getPlayerData() {
    return JSON.parse(localStorage.getItem('player'));
  }

  return { saveToken, getToken, setPlayerData, getPlayerData }
}