//using Microsoft.AspNetCore.ResponseCompression;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add SQLite and Identity services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false; // no need for user consent
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});


builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeFolder("/"); // Requires auth for all pages
    // If you want to exclude Index
    //options.Conventions.AllowAnonymousToPage("/Index");
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//just for debug, to be removed later
app.UseStatusCodePages();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();


app.UseAuthorization();

app.MapRazorPages();

app.MapHub<ChatHub>("/chatHub").RequireAuthorization(); ;
app.MapHub<LoginHub>("/LoginHub");

app.Run();
