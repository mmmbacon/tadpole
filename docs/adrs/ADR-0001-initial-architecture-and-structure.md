# ADR 0001: Initial Architecture and Project Structure

- Status: Accepted
- Date: 2026-06-11

## Context

The Tadpole v1.0 architecture defines a guardian-supervised, real-time 1:1 messaging platform with:
- ASP.NET Core API (REST + SignalR)
- EF Core with PostgreSQL
- Redis for SignalR backplane and session state
- Two Blazor WebAssembly clients (Guardian and Child)
- Strict RBAC (Guardian, Child, Admin)

## Decision

We scaffold the repository with:
- `src/Tadpole.Api` (ASP.NET Core API, SignalR hub at `/hub/msg`)
- `src/Tadpole.Infrastructure` (EF Core `AppDbContext`, Npgsql provider)
- `src/Tadpole.Application` (service abstractions)
- `src/Tadpole.Domain` (entities for guardians, children, connections, messages)
- `tests/Tadpole.UnitTests` (xUnit)
- Documentation under `docs/`, with ADRs in `docs/adrs/` and thinking notes in `docs/chatter/`.

Target framework set to `net10.0` per the architecture document's .NET 10 (LTS) guidance.

## Consequences

- Clear layering and separation of concerns aligned to domain verticals.
- Ready path to add Identity, JWT auth, and Redis backplane.
- CI can target .NET 10 and run unit tests on pull requests to `main`.

## Alternatives Considered

- Single project monolith: rejected to preserve layering and testability.
- Using another frontend stack (e.g., React): rejected to maintain end-to-end .NET consistency via Blazor WASM.
