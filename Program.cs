using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using TuLote.Data;
using TuLote.Models;
using TuLote.Seed;
using TuLote.Servicios;

var builder = WebApplication.CreateBuilder(args);
var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
// Add services to the container.
builder.Services.AddControllersWithViews(async opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
});

builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddAuthentication();

//builder.Services.AddIdentity<Usuario, IdentityRole>(opciones =>
//opciones.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IServicio_API_Provincia, Servicio_API_Provincia>();
builder.Services.AddScoped<IServicio_API_Municipio, Servicio_API_Municipio>();
builder.Services.AddScoped<IServicio_API_Localidad, Servicio_API_Localidad>();

//Se agrega servicio de autenticación  de usuario
builder.Services.AddAuthentication();
//se agrega el servicio Identity
builder.Services.AddIdentity<Usuario, IdentityRole>(opciones =>
opciones.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
//para poder crear mis propias vistas de login y registro sin usar las predeterminadas...la pagina inicia aqui
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opciones =>
    {
        opciones.LoginPath = "/usuarios/login";
        opciones.AccessDeniedPath = "/usuarioslLogin";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<Usuario>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await ContextSeed.SeedRolesAsync(userManager, roleManager);
        await ContextSeed.SeedAdminAsync(userManager, roleManager); //lo agrego luego de sembrar lo anterior
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }


}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Lotes}/{action=Index}/{id?}");

app.Run();


