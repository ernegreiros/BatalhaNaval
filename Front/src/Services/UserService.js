export default function UserService() {
  function saveToken(token) {
    localStorage.setItem('user-token', token);
  }

  function getToken() {
    return localStorage.getItem('user-token');
  }

  return { saveToken, getToken }
}