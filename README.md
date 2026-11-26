# BugTraq

### Requirements
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) 20.19+ or 22.12+ (includes npm)

### Running
```sh
$ cd [CloneFolder]/code/src/BugTraq.Api/ClientApp
$ npm install
$ cd ..
$ dotnet run
```

Open http://localhost:5080. In development, ASP.NET Core starts the Vite dev
server (http://localhost:5173) for you and redirects the browser to it; API
calls are proxied back to the .NET app.

### Tests
```sh
$ cd [CloneFolder]/code
$ dotnet test
```

### Publishing
`dotnet publish` builds the Vue app and serves it from `wwwroot`.
