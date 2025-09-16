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

            var role = await _roles.ResolveAsync(user.Id);

            _session.Set(user.Id, role);

            var target = role switch
            {
                UserRole.AdminProg => Url.Content("~/Mantenimientos/Index"),
                UserRole.Cliente => Url.Content("~/Clientes/MisVehiculos"),
                _ => Url.Content("~/")
            };

            return LocalRedirect(target);
        }
    }
}
