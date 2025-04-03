using Microsoft.EntityFrameworkCore;
using Z_Team_Group_Project.Data;
using Z_Team_Group_Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<Z_Team_Group_ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Z_Team_Group_ProjectContext")
        ?? throw new InvalidOperationException("Connection string 'Z_Team_Group_ProjectContext' not found.")));

// Register custom services
builder.Services.AddScoped<IZTeamAPIService, ZTeamAPIService>();

// Register BlackJackGame as a singleton (one instance for the entire application)
builder.Services.AddScoped<BlackJackGame>();

// Add session management
builder.Services.AddDistributedMemoryCache();  // In-memory cache to store session data
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Session expiration time
    options.Cookie.HttpOnly = true;  // Ensures cookie is not accessible via JS
    options.Cookie.IsEssential = true;  // Ensures cookie is sent even if consent is not given
});

// Add controllers and views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  // Enable HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection();  // Redirect HTTP requests to HTTPS
app.UseStaticFiles();  // Serve static files (e.g., CSS, JS, images)
app.UseRouting();  // Enable routing
app.UseAuthorization();  // Enable authorization
app.UseSession();  // Enable session middleware

// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();