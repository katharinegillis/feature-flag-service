This is a simple feature flag microservice that offers endpoints for other microservices in the ecosystem to get feature flag information.

## Usage

The feature flag data is stored in a sqlite file at /data. Use a volume to persist that sqlite file to the host.

```yaml
services:
  featureflagservice:
    image: kcordes/featureflagservice
    volumes:
      - ./.data:/data
```

## Configuration

There are console commands that can be used to set up, edit, or remove feature flags while the container is running. To show the help information, use `console --help` (ex. if using the above docker compose configuration, `docker compose exec featureflagservice console --help`).