using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarpentryShop.Services;
using CarpentryShop.Areas.Identity.Data;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CarpentryShopIdentityDbContextConnection");
builder.Services.AddDbContext<CarpentryShopIdentityDbContext>(options =>
    options.UseSqlite(connectionString));


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

app.Run();
