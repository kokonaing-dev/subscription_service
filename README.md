# Subscription Service

## Running Instructions

### Mode A: Local Development
1. Start DB:
   `docker compose -f docker-compose.db.yml up -d`
2. Apply Migrations:
   `dotnet ef database update`
3. Run API:
   `dotnet run`

_The API will start locally._

### Mode B: Full Stack Docker
1. Run:
   `docker compose up --build -d`
2. Requirement:
   The API waits for the DB to be healthy (see `depends_on` + `healthcheck` in `docker-compose.yml`).
3. Port:
   API is exposed on port `8000`.
4. URL:
   Swagger available at `http://localhost:8000/swagger`.
