# Carpentry Shop - Web Application
This is a web application designed to manage and track the progress of
carpentry orders. It also includes a feature that notifies the individual who
placed the order once it has been completed.

![image](https://raw.githubusercontent.com/h3ndry/carpentry-shop/master/screenshot/homepage.png)

*Live at* [https://carpentry.simanga.dev/](https://carpentry.simanga.dev/)

## Features

* Admin Access and Dashboard for efficient order management
* Notification feature for the individual who placed the order upon completion (disabled by default)
* Capability to order multiple carpentry items in a single order
* Configured CI/CD for seamless integration and deployment
* Utilizes Entity Framework Core and Postgres SQL
* Features a responsive design for optimal user experience

## Project Setup

The web application is constructed using .Net Core 6 and Postgres SQL. In order
to run the application locally, it's necessary to have the .Net Core runtime
installed on your machine. You can verify the presence of dotnet on your system
by executing the command provided below in your terminal.

```bash
# check dotnet cli tool if it install
$ dotnet --version

# check if the .net core runtime 6 is available
dotnet --list-runtimes
````
Next, you'll need to set up `environment variables` for database connectivity, as outlined below.
```shell
export POSTGRES_PASSWORD=
export POSTGRES_USER=
export POSTGRES_DB=
export ASPNETCORE_URLS=http://+:5000
export DB_CONNECTION="Host=db;Database=${POSTGRES_DB};Username=${POSTGRES_USER};Password=${POSTGRES_PASSWORD};Port=5432"
```

### Run local dev

```
$ dotnet ef database update

$ dotnet build

$ dotnet run
```

## Production deployment with Dokploy

The root `docker-compose.yml` runs the .NET 10 app and PostgreSQL 17. Dokploy's
Traefik proxy handles the public host and TLS, so the stack has no reverse proxy
container and publishes no host ports. Aspire AppHost is used for local
development, not as a production container.

Create a Docker Compose service in Dokploy that points at this repository, set
the compose path to `docker-compose.yml`, and add these environment variables in
the Environment tab:

```bash
APP_DOMAIN=carpentry.simanga.dev
POSTGRES_PASSWORD=<openssl rand -hex 32>
ADMIN_EMAIL=admin@simanga.dev
ADMIN_PASSWORD=<unique password>
```

`just env` or `./deploy/init-env.sh` generates a `.env` with the same keys for
local use. On Dokploy, paste the values into the UI. Compose refuses to start
while a required value is empty.

In the Domains tab, add one domain:

- Host: `carpentry.simanga.dev`
- Service: `app`
- Container port: `8080`
- HTTPS: on, Let's Encrypt

Redeploy after changing domains. Do not publish ports for the app; Traefik
reaches `app:8080` over the platform network.

The app trusts `X-Forwarded-*` headers from the platform proxy, so redirects and
generated links use `https` and the public host. `AllowedHosts` accepts
`APP_DOMAIN` and `localhost`.

Use a hex database password to avoid connection-string escaping problems.
Quote admin passwords containing `$` or `#` with single quotes in `.env`.
Environment variables are visible to Docker administrators; restrict access
to the host and Docker socket. For stricter secret-management requirements,
integrate a secret store rather than storing credentials in environment variables.

The configured admin account is created only if it does not already exist.
Changing `ADMIN_PASSWORD` later does not reset that account's password. Existing
databases may still contain the old `admin@localhost.com` account with the old
default password. Reset or remove it before exposing the deployment.

Named volumes preserve PostgreSQL data and ASP.NET Core data protection keys
across container replacements. Back them up securely. Do not use
`docker compose down -v` unless you intend to delete them. Data protection keys
are stored unencrypted on the volume, so protect backups and host storage.

### Database backups and updates

```bash
mkdir -p backups
chmod 700 backups
docker compose exec -T db pg_dump -U carpentry -d carpentrydb -Fc > backups/carpentrydb.dump
```

Copy backups off the server and test restoring them. Keep the backup directory
out of version control and the Docker build context.

The app currently calls `EnsureCreated()` on startup. It can initialize a fresh
database, but it does not apply schema changes to an existing database. Review
and test a migration strategy before deploying schema changes. Do not blindly
run EF migrations against a database initialized by `EnsureCreated()`.

After backing up, deploy app updates by pushing to the branch Dokploy tracks
and clicking Redeploy, or by running:

```bash
docker compose up -d --build --wait
```

This is a single-server deployment, not high availability. Pin image digests
and use versioned `APP_TAG` values for controlled releases. Schedule image
updates and off-server backups. The Aspire dashboard is deliberately not
deployed or publicly exposed, and telemetry is not retained by this setup.

## Emails

The email functionality has been disabled due to the complexities involved in
configuring Gmail for programmatic email sending. However, if you have a more
user-friendly email provider, it will be significantly simpler to get it to
work.

### Todos

- [x] Add a link to the github source code link on the homepage of the app
- [ ] Add a Fav Icon on the address bar of the app
- [ ] Update the application to the latest framework
- [ ] text

