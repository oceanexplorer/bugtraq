import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// In development the ASP.NET Core app (http://localhost:5080) launches this dev server
// via SpaProxy; API calls are proxied back to it. SpaProxy passes its environment on,
// so ASPNETCORE_URLS tells us where the API is listening.
const apiUrl = process.env.ASPNETCORE_URLS?.split(';')[0] ?? 'http://localhost:5080'

export default defineConfig({
  plugins: [vue()],
  server: {
    port: 5173,
    strictPort: true,
    proxy: {
      '/api': apiUrl,
      '/health': apiUrl
    }
  }
})
