import axios from 'axios'

// Relative base URL: served by ASP.NET Core in production, proxied by Vite in development.
export default axios.create({ baseURL: '/api' })
