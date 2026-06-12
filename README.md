# Tadpole

Tadpole is a guardian-supervised, 1:1 real-time messaging platform for kids. Guardians create and manage child accounts, approve social connections, and can review or revoke those connections. Children use a simplified messaging experience and can only communicate with guardian-approved friends.

## Architecture

The initial scaffold follows the architecture document in `docs/`:

- `src/Tadpole.Api` - ASP.NET Core API, REST endpoints, and SignalR hub host.
- `src/Tadpole.Application` - Application service contracts and use-case boundaries.
- `src/Tadpole.Domain` - Core domain entities and enums.
- `src/Tadpole.Infrastructure` - EF Core persistence and infrastructure integrations.
- `tests/Tadpole.UnitTests` - Unit tests.

The target stack is .NET 10, ASP.NET Core, SignalR, EF Core, PostgreSQL, Redis, and Blazor WebAssembly clients for Guardian and Child experiences.

## Requirements

- .NET 10 SDK
- Docker Desktop or a compatible Docker runtime
- PostgreSQL 16 (local service or Docker Compose)
- Redis 7 (local service or Docker Compose)
- Cursor or VS Code with:
  - C# Dev Kit
  - .NET Install Tool
  - Docker extension
- Optional for future media work: MinIO or another S3-compatible object store

## Documentation

All project documentation belongs under `docs/`.

- ADRs live in `docs/adrs/`.
- The ADR template is `docs/adrs/ADR_TEMPLATE.md`.
- Planning or thinking notes belong in `docs/chatter/`.

## Development

Install the .NET 10 SDK, then restore, build, and test:

```bash
dotnet restore ./tests/Tadpole.UnitTests/Tadpole.UnitTests.csproj
dotnet build ./tests/Tadpole.UnitTests/Tadpole.UnitTests.csproj
dotnet test ./tests/Tadpole.UnitTests/Tadpole.UnitTests.csproj
```

The API entry point is `src/Tadpole.Api/Program.cs`, with a health endpoint at `/health` and the initial SignalR message hub mapped to `/hub/msg`.

## CI

GitHub Actions runs tests for pull requests targeting `main` via `.github/workflows/ci.yml`.

## Repository Guidance

Cursor agents should not commit or push code to GitHub. Human maintainers own all commits and pushes.
