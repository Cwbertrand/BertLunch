using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model;
using BertLunch.Services;
using Stripe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (environment == "Development")
{
    var connectionString = builder.Configuration.GetConnectionString("BertLunchContextConnection")
                        ?? throw new InvalidOperationException("Connection string 'BertLunchContextConnection' not found.");
    builder.Services.AddDbContext<BertLunchContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}
else
{
    // var connectionString = builder.Configuration.GetConnectionString("BertLunchContextConnection")
    //                     ?? throw new InvalidOperationException("Connection string 'BertLunchContextConnection' not found.");
    // builder.Services.AddDbContext<BertLunchContext>(options =>
    //     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}


builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BertLunchContext>();

// Defining the role policy
builder.Services.AddAuthorization(opt => opt.AddPolicy("Admin", policy => policy.RequireRole("Admin")));


// Filtering routes with roles
builder.Services.AddControllersWithViews(opt => opt.Filters.Add(new RoutingRestriction()));
builder.Services.AddRazorPages(opt => opt.Conventions.AuthorizeFolder("/Admin", "Admin"));

//Session with cookie
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Registered Basket service
builder.Services.AddScoped<BasketService>();

// QuestPdf licence for the free version
QuestPDF.Settings.License = LicenseType.Community;

// Register custom PDF generator service
builder.Services.AddScoped<PdfGeneratorService>();

// Default redirect the user to this route if the user isn't connected
builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
{
    options.LoginPath = "/Account/Security/Login";
});

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AccessDeniedPath = "/";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDefaultFiles();
app.UseStaticFiles();


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
//app.MapRazorPages("Admin", "/Admin");

// Stripe configuration
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>(); 

// Automatically querying the database when the application starts
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<BertLunchContext>();
    await context.Database.MigrateAsync();
    //await SeedingData.Seed(context);
}
catch (Exception e)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "An error occurred during migration");
}
app.Run();
