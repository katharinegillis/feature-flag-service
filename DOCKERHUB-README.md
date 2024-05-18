This is a simple feature flag microservice that offers endpoints for other microservices in the ecosystem to get feature flag information.

## Usage: Sqlite

The feature flag data is stored in a sqlite file at /data by default. Use a volume to persist that sqlite file to the host.

```yaml
services:
  featureflagservice:
    image: kcordes/featureflagservice
    volumes:
      - ./.data:/data
```

## Usage: Split.Io

The feature flag data is stored with Split.Io. You will need an account with them, and get a server-side SDK key. Then, configure the image as follows:

```yaml
services:
  featureflagservice:
    image: kcordes/featureflagservice
  volumes:
    - ./.data:/data
  environment:
    SPLITIO__SDKKEY: <the server-side sdk key from your split.io account>
    SPLITIO__TREATMENTKEY: <the user identifier you want to use ex. default>
```

If the image fails to connect to Split.IO in 10 seconds after starting up, it will default back to the Sqlite configuration.

## Configuration

There are console commands that can be used to set up, edit, or remove feature flags while the container is running. To show the help information, use `console --help` (ex. if using the above docker compose configuration, `docker compose exec featureflagservice console --help`).

Note that if you are using the Split.Io datastore, then only the get and list commands will work as the Split.Io SDK is read-only. You must configure your feature flags within Split.Io instead of with the console commands.