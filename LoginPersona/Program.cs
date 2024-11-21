
var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de servicios MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuraci�n de enrutamiento
app.UseRouting();

app.UseStaticFiles();  // Para servir archivos est�ticos como CSS y JS

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Persona}/{action=Login}/{id?}");

app.Run();