using HospitalDeVehiculosUltimaVersion.Factory.FactoryRoll;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Pages.Registrar
{
    public class RegistroModel : PageModel
    {
        private readonly HospitalDeVehiculosContext _context;
        private readonly IRoleResolver _roles;
        private readonly ICurrentUserSession _session;
        [BindProperty]
        public byte tipoUsuario { get; set; }
        [BindProperty]
        public Usuario Usuario { get; set; }

        public RegistroModel(HospitalDeVehiculosContext context, IRoleResolver roles, ICurrentUserSession session)
        {
            _context = context;
            _roles = roles;
            _session = session;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
                return Page();

            Usuario usuario = new Usuario
            {
                PrimerNombre = Usuario.PrimerNombre,
                SegundoNombre = Usuario.SegundoNombre,
                PrimerApellido = Usuario.PrimerApellido,
                SegundoApellido = Usuario.SegundoApellido,
                Ci = Usuario.Ci,
                NumeroContacto = Usuario.NumeroContacto,
                Email = Usuario.Email,
                Contrasenia = Usuario.Contrasenia,
                Estado = 1
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            if (tipoUsuario == 0)
            {
                Empleado empleado = new Empleado
                {
                    IdNavigation = usuario,
                    Direccion = "direccion",
                    SalarioBasico = 2500,
                    Cargo = "Administrador de Programacion"
                };
                _context.Empleados.Add(empleado);
                await _context.SaveChangesAsync();
                AdministradorDeProgramacion adminProgramacion = new AdministradorDeProgramacion
                {
                    IdNavigation = empleado
                };
                _context.AdministradorDeProgramacions.Add(adminProgramacion);
            }
            else if (tipoUsuario == 1)
            {
                Empleado empleado = new Empleado
                {
                    IdNavigation = usuario,
                    Direccion = "direccion",
                    SalarioBasico = 2500,
                    Cargo = "Administrador de Repuesto"
                };
                _context.Empleados.Add(empleado);
                await _context.SaveChangesAsync();
                AdministradorDeRepuesto administradorDeRepuesto = new AdministradorDeRepuesto
                {
                    IdNavigation = empleado
                };
                _context.AdministradorDeRepuestos.Add(administradorDeRepuesto);
            }
            else if (tipoUsuario == 2)
            {
                Cliente cliente = new Cliente
                {
                    IdNavigation = usuario
                };
                _context.Clientes.Add(cliente);
            }
            await _context.SaveChangesAsync();
            return Redirect("/Login");
        }
    }
}
