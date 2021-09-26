export const apiVersion = 'v1';
export const server =
  process.env.REACT_APP_ENV === 'production'
    ? 'https://todo-backend-app.azurewebsites.net'
    : process.env.REACT_APP_ENV === 'dockerprod'
    ? 'https://localhost:8090'
    : 'https://localhost:5001';
