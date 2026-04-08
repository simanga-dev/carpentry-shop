using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarpentryShop.Services;
using CarpentryShop.Areas.Identity.Data;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults (Aspire)
builder.AddServiceDefaults();

// Configure PostgreSQL database connection
// When running with Aspire, the connection string is automatically configured
// When running standalone, it falls back to environment variable or configuration
var connectionString = builder.Configuration.GetConnectionString("carpentrydb");

if (string.IsNullOrEmpty(connectionString))
{
    // Fallback to legacy environment variable for backward compatibility
    connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
    
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new Exception(
            "Database connection not configured. Either:\n" +
            "1. Run with Aspire: install Aspire CLI (curl -sSL https://aspire.dev/install.sh | bash) and use 'aspire run' or 'just run'\n" +
            "2. Set DB_CONNECTION environment variable with PostgreSQL connection string\n" +
            "3. Configure ConnectionStrings:carpentrydb in appsettings.json"
        );
    }
    
    // Register DbContext manually when not using Aspire
    builder.Services.AddDbContext<CarpentryShopIdentityDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    // Use Aspire PostgreSQL integration when connection string is provided by Aspire
    builder.AddNpgsqlDbContext<CarpentryShopIdentityDbContext>("carpentrydb");
}

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CarpentryShopIdentityDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var emailSection = builder.Configuration.GetSection("EmailSettings");
builder.Services.Configure<EmailSettings>(emailSection);
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizePage("/Admin/Index");
        });

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CarpentryShopIdentityDbContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// using Microsoft.AspNetCore.HttpOverrides;

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SampleData.InitializeAsync(services);
}

app.MapRazorPages();

// Map Aspire default endpoints
app.MapDefaultEndpoints();

app.Run();
