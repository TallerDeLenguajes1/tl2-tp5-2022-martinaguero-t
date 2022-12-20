using System.Collections.Specialized;
using AutoMapper;
using TP4.Models;
using TP4.Repositories;
using NLog;
using NLog.Web;

// Logger de NLog. Trabajando con inyección de dependencia.
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Para AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Inyección de dependencias para los repositorios creados
builder.Services.AddTransient<IRepositorioCadetes, RepositorioCadetes>();
builder.Services.AddTransient<IRepositorioClientes, RepositorioClientes>();
builder.Services.AddTransient<IRepositorioPedidos, RepositorioPedidos>();
builder.Services.AddTransient<IRepositorioUsuarios, RepositorioUsuarios>();

// Para trabajar con session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}
);

// NLog: Inyección de dependencia
builder.Logging.ClearProviders();
builder.Host.UseNLog();

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

app.UseAuthorization();

// Para trabajar con session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
