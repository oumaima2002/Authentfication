using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sign.Data;

var builder = WebApplication.CreateBuilder(args);

// Ajoutez le service DbContext avec MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(5, 2, 0)) // Version de votre MySQL
    )
);

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); // Utilisation d'une cache en mémoire pour la session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Timeout de la session
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Si vous avez une authentification
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");


app.Run();
