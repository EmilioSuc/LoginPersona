
var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración de enrutamiento
app.UseRouting();

app.UseStaticFiles();  // Para servir archivos estáticos como CSS y JS

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Persona}/{action=Login}/{id?}");

app.Run();