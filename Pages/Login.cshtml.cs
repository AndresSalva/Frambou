using HospitalDeVehiculosUltimaVersion.Factory.FactoryRoll;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HospitalDeVehiculosUltimaVersion.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HospitalDeVehiculosContext _context;
        private readonly IRoleResolver _roles;
        private readonly ICurrentUserSession _session;

        public LoginModel(HospitalDeVehiculosContext context, IRoleResolver roles, ICurrentUserSession session)
        {
            _context = context;
            _roles = roles;
            _session = session;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public void OnGet() { }

        public class InputModel
        {
            [Required, Display(Name = "Usuario o correo")]
            public string UserNameOrEmail { get; set; } = string.Empty;

            [Required, Display(Name = "Contraseña")]
            public string Password { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    u.Email == Input.UserNameOrEmail &&
                    u.Contrasenia == Input.Password &&
                    u.Estado == 1);

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas o usuario no existe.");
                return Page();
            }

            // 2) Resolver rol a partir del id (Usuario -> Empleado/AdminProg | Cliente)
            var role = await _roles.ResolveAsync(user.Id);

            // 3) Guardar sesión de forma tipada (sin magic strings)
            _session.Set(user.Id, role);

            // 4) Redirigir según rol (rutas de ejemplo; ajusta a tus páginas reales)
            var target = role switch
            {
                UserRole.AdminProg => Url.Content("~/Mantenimientos/Index"),
                UserRole.Cliente => Url.Content("~/Clientes/MisVehiculos"),
                _ => Url.Content("~/")
            };

            return LocalRedirect(target);

            //codigo caca sin SOLID:
            //var esAdminProg = await _context.Set<AdministradorDeProgramacion>()
            //                        .AsNoTracking()
            //                        .AnyAsync(a => a.Id == user.Id);

            //var esCliente = await _context.Set<Cliente>()
            //                                .AsNoTracking()
            //                                .AnyAsync(c => c.Id == user.Id);

            //// 3) Guardar lo mínimo indispensable en Session
            //HttpContext.Session.SetInt32("UsuarioId", user.Id);

            //if (esAdminProg)
            //{
            //    HttpContext.Session.SetString("Rol", "AdminProg");
            //    HttpContext.Session.SetInt32("AdminProgId", user.Id);
            //    return LocalRedirect(Url.Content("~/Mantenimientos/Index"));
            //}
            //else if (esCliente)
            //{
            //    HttpContext.Session.SetString("Rol", "Cliente");
            //    HttpContext.Session.SetInt32("ClienteId", user.Id);
            //    return LocalRedirect(Url.Content("~/Clientes/MisVehiculos"));
            //}
            //else
            //{
            //    HttpContext.Session.SetString("Rol", "Desconocido");
            //    return LocalRedirect(Url.Content("~/"));
            //}
        }
    }
}
