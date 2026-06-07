# AGENTS.md

## Cursor Cloud specific instructions

### Product overview

**Enad Web APP** is an ASP.NET Core 8 Razor Pages app with cookie authentication and a login page. Application code lives on branch `cursor/enad-login-page-6a3d` (not yet merged to `main`, which currently only has a README).

### Prerequisites

- **.NET 8 SDK** — required to build and run. If `dotnet` is missing, install with the [official install script](https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual#scripted-install) to `~/.dotnet` and ensure `/usr/local/bin/dotnet` (or `~/.dotnet`) is on `PATH`.

### Dependency refresh

See the VM update script (`dotnet restore` when `EnadWebApp/EnadWebApp.csproj` exists). No npm, Docker, or database services are required.

### Running the app

From `EnadWebApp/`:

```bash
dotnet run --urls http://localhost:5036
```

Default dev URL: **http://localhost:5036** (see `Properties/launchSettings.json`; README mentions port 5000 but the project profile uses 5036).

Login: **http://localhost:5036/Account/Login**

Default credentials (from `appsettings.json`):

| Field    | Value           |
|----------|-----------------|
| Email    | admin@enad.com  |
| Password | Password123!    |

### Lint / test / build

| Task   | Command |
|--------|---------|
| Restore | `dotnet restore EnadWebApp/EnadWebApp.csproj` |
| Build   | `dotnet build EnadWebApp/EnadWebApp.csproj` |
| Run     | `dotnet run --project EnadWebApp --urls http://localhost:5036` |

There is no separate linter or test project in the repo; `dotnet build` is the primary quality gate.

### Gotchas

- Checkout `cursor/enad-login-page-6a3d` (or merge it) before working on the app if `main` has no `EnadWebApp/` folder.
- Use a long-running shell (tmux) for `dotnet run`; the server blocks the terminal.
- Razor Pages login POST requires an antiforgery token; use browser or curl with token extraction for API-style tests.
