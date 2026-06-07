# Enad Web APP

ASP.NET Core Razor Pages web application with a login page.

## Getting Started

```bash
cd EnadWebApp
dotnet run
```

Open `http://localhost:5000/Account/Login` in your browser.

## Default Login Credentials

| Field    | Value           |
|----------|-----------------|
| Email    | admin@enad.com  |
| Password | Password123!    |

Credentials can be changed in `EnadWebApp/appsettings.json` under the `Login` section.

## Project Structure

- `EnadWebApp/Pages/Account/Login.cshtml` — Login page
- `EnadWebApp/Pages/Shared/_LoginLayout.cshtml` — Login page layout
- `EnadWebApp/Program.cs` — Cookie authentication configuration
