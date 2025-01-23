using DotNetEnv;
using Dunware.Blog.Client.Pages;
using Dunware.Blog.Components;
using Dunware.Blog.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables del archivo .env solo en desarrollo
if (builder.Environment.IsDevelopment())
{
    Env.Load(); // Cargar variables desde el archivo .env
}

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Obtener la URL base del servidor
var urls = builder.WebHost.GetSetting(WebHostDefaults.ServerUrlsKey)?.Split(';');
var baseUrl = urls?.FirstOrDefault() ?? "https://localhost:7168"; // Valor por defecto
if(!builder.Environment.IsDevelopment())
{
    baseUrl = Environment.GetEnvironmentVariable("BASE_URL");
}


builder.Services.AddHttpClient("ServerAPI", client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

// Obtener la cadena de conexión desde la variable de entorno
var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION");

// Configurar el DbContext con la cadena de conexión
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseNpgsql(connectionString));

// Servicio predeterminado para HttpClient
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Dunware.Blog.Client._Imports).Assembly);
app.MapControllers();

app.Run();
