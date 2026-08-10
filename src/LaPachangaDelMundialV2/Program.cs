using LaPachangaDelMundialV2.Components;
using LaPachangaDelMundialV2.Utils;
using LaPachangaDelMundialV2.Controllers;
using LaPachangaDelMundialV2.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Servicios propios del sistema
builder.Services.AddSingleton<SesionUsuario>();
builder.Services.AddSingleton<UsuarioController>();
builder.Services.AddSingleton<PartidoController>();
builder.Services.AddSingleton<QuinielaController>();
builder.Services.AddSingleton<PronosticoController>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
