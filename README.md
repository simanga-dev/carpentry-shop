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

## Production deployment with Docker Compose

The root `docker-compose.yml` runs the .NET 10 app, PostgreSQL 17, and Caddy
with automatic HTTPS. Aspire AppHost is used for local development, not as a
production container. Compose supplies the same `ConnectionStrings:carpentrydb`
configuration that Aspire supplies locally.

On a single Linux server with Docker Engine and the Compose plugin:

```bash
just env            # or: ./deploy/init-env.sh
# Edit .env before a public deployment: APP_DOMAIN, ACME_EMAIL,
# ADMIN_EMAIL, ADMIN_PASSWORD, and APP_BIND_ADDRESS. Never commit .env.
docker compose config --quiet
docker compose up -d --build --wait
docker compose ps
docker compose logs --tail=100 app
```

`just env` fills `.env` with a random database password and admin password and
prints the admin credentials once. It never overwrites an existing `.env`. If you
prefer to do it by hand, copy `.env.example` and fill in every value; Compose
refuses to start while a required value is empty.

Caddy publishes ports 80 and 443 on `127.0.0.1` by default, so the stack is
reachable only from the host. On a public server set `APP_BIND_ADDRESS=0.0.0.0`
in `.env` so Caddy can answer on the domain and complete the ACME challenge.

Point your domain's DNS A record at the server. If you publish an AAAA record,
it must also reach this server. With `APP_BIND_ADDRESS=0.0.0.0`, allow inbound
TCP ports 80 and 443. These ports must be free for Caddy. The app and database
have no host-published ports.
The frontend subnet is `172.30.0.0/24`. If it conflicts with your network,
change the subnet, the proxy and app addresses, and
`ForwardedHeaders__KnownProxies__0` together.

Use a hex database password to avoid connection-string escaping problems.
Quote admin passwords containing `$` or `#` with single quotes in `.env`.
Environment variables are visible to Docker administrators; restrict access
to the host and Docker socket. For stricter secret-management requirements,
integrate a secret store rather than storing credentials in environment variables.

The configured admin account is created only if it does not already exist.
Changing `ADMIN_PASSWORD` later does not reset that account's password. Existing
databases may still contain the old `admin@localhost.com` account with the old
default password. Reset or remove it before exposing the deployment.

Named volumes preserve PostgreSQL data, TLS certificates, and ASP.NET Core data
protection keys across container replacements. Back them up securely. Do not use
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

After backing up, deploy app updates with:

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

