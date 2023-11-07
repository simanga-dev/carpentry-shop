# Carpentry Shop - Web Application
This is a web application designed to manage and track the progress of
carpentry orders. It also includes a feature that notifies the individual who
placed the order once it has been completed.

![image](https://raw.githubusercontent.com/h3ndry/carpentry-shop/master/screenshot/homepage.png)

*Live at* [http://178.79.147.9:8080/](http://178.79.147.9:8080/)

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

## Emails

The email functionality has been disabled due to the complexities involved in
configuring Gmail for programmatic email sending. However, if you have a more
user-friendly email provider, it will be significantly simpler to get it to
work.

