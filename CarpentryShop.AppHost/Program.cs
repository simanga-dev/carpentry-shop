var builder = DistributedApplication.CreateBuilder(args);

// Add PostgreSQL database
var postgres = builder.AddPostgres("postgres")
    .WithImageTag("17");

var carpentryDb = postgres.AddDatabase("carpentrydb");

// Add the web application with reference to the database
builder.AddProject("carpentryshop", "../CarpentryShop.csproj")
    .WithReference(carpentryDb)
    .WaitFor(carpentryDb);

builder.Build().Run();
