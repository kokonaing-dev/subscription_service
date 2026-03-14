# Subscription Service

Subscription management API built with ASP.NET Core (.NET 9), PostgreSQL, EF Core, and OData.

## Features
- CRUD for subscriptions, plans, payment gateways, discounts, transactions, and related entities.
- OData endpoints for filtering, ordering, and expansion.
- Docker setup with automatic migrations and seed data.

## Prerequisites
- .NET SDK 9
- Docker + Docker Compose

## Quick Start (Docker)
1. Build and run:
```bash
docker compose up --build -d
```
2. Open Swagger:
```
http://localhost:8000/swagger
```

Notes:
- The API waits for the DB to be healthy before starting.
- Migrations run via a bundled `dotnet-ef` executable on container startup.
- Seed data is loaded from `docker/init.sql` on first DB initialization.

## Local Development
1. Start Postgres:
```bash
docker compose -f docker-compose.db.yml up -d
```
2. Apply migrations:
```bash
dotnet ef database update
```
3. Run the API:
```bash
dotnet run
```

## Configuration
The API uses `ConnectionStrings:DefaultConnection`.

Docker defaults (from `docker-compose.yml`):
- Host: `postgres`
- Port: `5432`
- Database: `subscriptiondb`
- Username: `postgres`
- Password: `postgres`

Local defaults (from `appsettings.json`):
- Host: `localhost`
- Port: `5432`
- Database: `subscriptiondb`
- Username: `postgres`
- Password: `postgres`

## OData
Base route:
```
/odata
```

Example:
```
/odata/plans?$filter=IsActive eq true&$orderby=Price desc
```

## Seed Data
Seed logic lives in `docker/init.sql`. It inserts:
- Payment gateways
- Discounts
- Plans
- Subscriptions
- Subscription transactions

The seed runs only when the Postgres data volume is created. To re-run seed:
```bash
docker compose down -v
docker compose up --build -d
```
Warning: `-v` deletes all database data.

## Troubleshooting
- If you see `relation "__EFMigrationsHistory" does not exist`, the DB is up but migrations have not run yet.
- If seed data fails, ensure the DB schema matches the latest migrations and re-run with a fresh volume.

## Project Layout
- `Controllers/` HTTP endpoints
- `Data/` EF Core DbContext
- `Models/` Entities and enums
- `Services/` Business logic
- `Migrations/` EF Core migrations
- `docker/` Container scripts and seed SQL
