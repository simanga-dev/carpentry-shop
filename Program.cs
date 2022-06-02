using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarpentryShop.Data;
using CarpentryShop.Services;

var builder = WebApplication.CreateBuilder(args);
// public IConfiguration Configuration { get; }


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var emailSection = builder.Configuration.GetSection("EmailSettings");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//                 .AddRoles<IdentityRole>()
//                 .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<EmailSettings>(emailSection);
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizePage("/Admin/Index");
        });

var app = builder.Build();


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
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     await SampleData.InitializeAsync(services);
// }

app.MapRazorPages();


app.Run();
