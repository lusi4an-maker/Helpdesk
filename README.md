# Helpdesk API

A ticketing system REST API built with **.NET 10 Minimal APIs** and **Entity Framework Core**. Built incrementally as a learning project, focused on getting authentication, authorization, and data modeling right — not just CRUD.

## Features

- **JWT authentication** with role claims embedded in the token.
- **Role-based authorization** across 5 roles (`Cliente`, `Agente`, `Analista`, `Administrador`, `Gerente`), enforced in two layers:
  - **Policy layer** — endpoint-level restrictions (e.g. only Admins/Managers can delete a ticket or manage users).
  - **Ownership/scope layer** — handler-level filtering so a Client only sees their own tickets, and an Agent/Analyst only sees tickets assigned to them.
- **Forced credential rotation on first login** — new users must change the password issued to them before accessing anything else, enforced via a claim + middleware gate.
- **Ticket assignment workflow** — Admins/Managers can assign or unassign a ticket to an Agent/Analyst, with role validation.
- **Response DTOs** — API responses never leak password hashes or internal flags; entities are projected to dedicated response models.
- **Admin bootstrap seeding** — the app seeds a default Administrator on first run if none exists, solving the chicken-and-egg problem of a fully auth-gated `/usuarios` endpoint.

## Stack

- .NET 10 / ASP.NET Core Minimal APIs
- Entity Framework Core 10 + SQL Server
- JWT Bearer authentication (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- `PasswordHasher<T>` (ASP.NET Core Identity) for password hashing
- Native .NET 10 validation (`AddValidation()` + Data Annotations)

## Project structure

```
src/Helpdesk.Api/
├── Endpoints/        # Minimal API endpoint groups (Tickets, Usuarios, Auth, TicketDetalle)
├── Models/            # EF Core entities + enums
├── Dtos/              # Request/response records
├── Data/              # DbContext + entity configuration
├── Authorization/      # Custom authorization policies
└── Migrations/         # EF Core migrations
```

## Getting started

### Prerequisites

- .NET 10 SDK
- SQL Server (local instance or container)

### Configuration (secrets)

This project uses [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) for local development — no secrets are committed to the repo. From `src/Helpdesk.Api`:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=Helpdesk;Trusted_Connection=True;TrustServerCertificate=True;"
dotnet user-secrets set "Jwt:Key" "<base64-encoded-signing-key>"
dotnet user-secrets set "SeedAdmin:Password" "<a password matching the password policy>"
```

Non-sensitive JWT settings (`Issuer`, `Audience`, `ExpiryMinutes`) already live in `appsettings.json`.

### Run

```bash
cd src/Helpdesk.Api
dotnet ef database update
dotnet run
```

On first run, a default Administrator user (`admin@helpdesk.com`) is seeded automatically using the `SeedAdmin:Password` secret.

## API overview

All endpoints except `/auth/login` and `/ping` require a valid JWT (`Authorization: Bearer <token>`).

| Method | Route | Access |
|---|---|---|
| `POST` | `/auth/login` | Public |
| `PUT` | `/auth/login/changepwd` | Authenticated |
| `GET` | `/tickets` | Authenticated (scoped by role) |
| `GET` | `/tickets/{id}` | Authenticated (scoped by role) |
| `POST` | `/tickets` | Authenticated |
| `PUT` | `/tickets/{id}` | Authenticated (owner/assignee/admin) |
| `DELETE` | `/tickets/{id}` | Admin/Manager |
| `PUT` | `/tickets/{id}/assign` | Admin/Manager |
| `PUT` | `/tickets/{id}/status` | Support staff (scoped by role) |
| `GET` | `/tickets/{ticketId}/detalles` | Authenticated (scoped by role) |
| `POST` | `/tickets/{ticketId}/detalles` | Support staff |
| `GET` | `/usuarios` | Authenticated |
| `GET` | `/usuarios/{id}` | Authenticated |
| `POST` | `/usuarios` | Admin/Manager |
| `PUT` | `/usuarios/{id}` | Admin/Manager |
| `DELETE` | `/usuarios/{id}` | Admin/Manager |
| `PUT` | `/usuarios/{id}/email` | Admin/Manager |
| `PUT` | `/usuarios/{id}/status` | Admin/Manager |

An OpenAPI document is available at `/openapi/v1.json` in the Development environment.

## Roadmap

Next up: a Blazor WebAssembly front end consuming this API.
