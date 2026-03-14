# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Generate a migration bundle (self-contained executable)
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef \
    && dotnet-ef migrations bundle --self-contained -r linux-x64 -o /app/migrate

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy the published app and migration bundle
COPY --from=build /app/publish .
COPY --from=build /app/migrate /app/migrate
COPY docker/entrypoint.sh /app/entrypoint.sh
COPY docker/init.sql /app/docker/init.sql

# Install PostgreSQL client
RUN apt-get update \
    && apt-get install -y --no-install-recommends postgresql-client \
    && rm -rf /var/lib/apt/lists/*

# Make migration bundle executable
RUN chmod +x /app/migrate

ENV ASPNETCORE_URLS=http://0.0.0.0:8000
RUN chmod +x /app/entrypoint.sh

EXPOSE 8000
ENTRYPOINT ["/app/entrypoint.sh"]
