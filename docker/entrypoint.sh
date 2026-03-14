#!/bin/bash
set -e

echo "Waiting for PostgreSQL to be ready..."
until pg_isready -h "$DB_HOST" -p "$DB_PORT" -U "$DB_USER"; do
  echo "PostgreSQL is unavailable - sleeping"
  sleep 2
done

echo "PostgreSQL is up - running migrations..."
/app/migrate --connection "Host=$DB_HOST;Port=$DB_PORT;Database=$DB_NAME;Username=$DB_USER;Password=$DB_PASSWORD"

echo "Running seed data..."
if [ -f "/app/docker/init.sql" ]; then
    echo "Seeding database from init.sql..."
    PGPASSWORD=$DB_PASSWORD psql -h "$DB_HOST" -p "$DB_PORT" -U "$DB_USER" -d "$DB_NAME" -f /app/docker/init.sql
    echo "Seed data completed."
else
    echo "Warning: init.sql not found at /app/docker/init.sql"
    ls -la /app/docker/ || echo "Docker directory not found"
fi

echo "Starting application..."
dotnet /app/subscription_service.dll