using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using SitioVentas.Repository.IRepository;
using SitioVentas.Repository.Repository;
using SitioVentas.Services;
using SitioVentas.Services.IServices;
using SitioVentas.Services.Services;
using System.Configuration;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDbConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new MySqlConnection(connectionString);
});

//Repositorios
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IGrupoRepository, GrupoRepository>();
builder.Services.AddTransient<ISubGrupoRepository, SubGrupoRepository>();
builder.Services.AddTransient<ITipoRepository, TipoRepository>();
builder.Services.AddTransient<IFotoRepository, FotoRepository>();

//Servicios
builder.Services.AddTransient<IItemService,ItemService>();
builder.Services.AddTransient<IBackupService, BackupService>();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddRazorPages();
builder.Services.AddOptions();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty;
});

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller}/{action=Index}/{id?}");
});


app.MapFallbackToFile("index.html"); ;

app.Run();
