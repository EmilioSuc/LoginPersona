using LoginPersona.Models;
using Microsoft.AspNetCore.Mvc;

namespace loginPersona.Controllers
{

    public class PersonaController : Controller
    {
        // Referencia a la lista estática//
        private static List<Persona> personas = PersonaControllers.personas;

        private static bool usuarioAutenticado = false;

        // Acción  login//
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Acción para procesar el login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "Sergio" && password == "1234")
            {
                usuarioAutenticado = true;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Credenciales incorrectas";
                return View();
            }
        }

        // Acción para cerrar sesión
        [HttpGet]
        public IActionResult Logout()
        {
            usuarioAutenticado = false;
            return RedirectToAction("Login");
        }

        // Acción para mostrar el listado de personas
        [HttpGet]
        public IActionResult Index()
        {
            if (!usuarioAutenticado)
                return RedirectToAction("Login");

            return View(personas);
        }

        // Crear persona
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Persona persona)
        {
            if (ModelState.IsValid)
            {
                persona.Id = personas.Count + 1;
                personas.Add(persona);
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        // Editar persona
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var persona = personas.FirstOrDefault(p => p.Id == id);
            if (persona == null)
                return NotFound();

            return View(persona);
        }

        [HttpPost]
        public IActionResult Edit(Persona persona)
        {
            if (ModelState.IsValid)
            {
                var personaExistente = personas.FirstOrDefault(p => p.Id == persona.Id);
                if (personaExistente != null)
                {
                    personaExistente.Nombre = persona.Nombre;
                    personaExistente.Apellido = persona.Apellido;
                    personaExistente.Edad = persona.Edad;
                    personaExistente.Correo = persona.Correo;
                }
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        // Eliminar persona
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var persona = personas.FirstOrDefault(p => p.Id == id);
            if (persona != null)
                personas.Remove(persona);

            return RedirectToAction("Index");
        }

        // Buscar personas por nombre
        [HttpPost]
        public IActionResult Search(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return View("Index", new List<Persona>());  // Si el nombre está vacío o es nulo, retornamos una lista vacía
            }
            var resultados = personas
                .Where(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return View("Index", resultados);
        }
    }

    public static class PersonaControllers
    {
        public static List<Persona> personas = new List<Persona>
        {
            new Persona { Id = 1, Nombre = "Juan", Apellido = "Garcia", Edad = 25, Correo = "juan15@gmail.com" },
            new Persona { Id = 1, Nombre = "Emilio", Apellido = "Suc", Edad = 30, Correo = "emilio_9@gmail.com" },
            new Persona { Id = 2, Nombre = "Jose", Apellido = "Galindo", Edad = 26, Correo = "jgalindo30@gmail.com" },
            new Persona { Id = 3, Nombre = "Carlos", Apellido = "Lopez", Edad = 28, Correo = "carlopez@gmai.com" }
        };
    }
}


