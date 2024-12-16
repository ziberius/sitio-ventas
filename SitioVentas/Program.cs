using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using SitioVentas.Repository.IRepository;
using SitioVentas.Repository.Repository;
using SitioVentas.Services;
using SitioVentas.Services.IServices;
using SitioVentas.Services.Services;
using System.Configuration;
using System.Data;
using System.Text;

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
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

//Servicios
builder.Services.AddTransient<IItemService,ItemService>();
builder.Services.AddTransient<IBackupService, BackupService>();
builder.Services.AddTransient<IGrupoService, GrupoService>();
builder.Services.AddTransient<ISubgrupoService, SubgrupoService>();
builder.Services.AddTransient<ITipoService, TipoService>();
builder.Services.AddTransient<ILoginService, LoginService>();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddRazorPages();
builder.Services.AddOptions();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

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
