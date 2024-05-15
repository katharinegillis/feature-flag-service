# feature-flag-service
A feature flag microservice for use in other projects.

## For Dev Work

Dependencies:
- dotnet core 8.0

1. Clone the main branch.
2. Navigate to the directory you cloned it into (the root of the project).
3. Run `dotnet restore` to install the dependencies on the projects.
4. Open the solution in your IDE of preference.

## For Dev Docker Build

Dependencies:
- docker
- docker compose
- traefik 2.0
- dnsmasq to redirect *.docker urls to localhost
- dotnet core 8.0 with ef tool installed

1. Clone the main branch.
2. Navigate to the directory you clone it into (the root of the project).
3. Create an empty file .data/featureFlagContext.db (ex. `touch .data/featureFlagContext.db`).
4. Change to the WebAPI project directory (ex. `cd WebAPI`).
5. Run `dotnet ef database update --connection "Data Source=../.data/featureFlagContext.db"` to create and update the database.
6. Change back to the solution root directory (ex. `cd ..`)
7. Run `docker compose --build -up -d`. Wait for the notification that the feature flag service is running.
8. Open your browser and navigate to http://featureflags.docker/swagger.

## Production

Automatically builds to the https://hub.docker.com/r/kcordes/featureflagservice image in DockerHub.
