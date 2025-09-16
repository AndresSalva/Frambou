using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalDeVehiculosUltimaVersion.Factory.FactoryRoll; // ICurrentUserSession, UserRole

namespace HospitalDeVehiculosUltimaVersion.Pages.Clientes
{
    public class MisVehiculosModel : PageModel
    {
        private readonly HospitalDeVehiculosContext _context;
        private readonly ICurrentUserSession _session;

        public MisVehiculosModel(HospitalDeVehiculosContext context, ICurrentUserSession session)
        {
            _context = context;
            _session = session;
        }

        public List<Vehiculo> Vehiculos { get; private set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            int? clienteId = null;

            try
            {
                var (userId, role) = _session.Get();
                if (role == UserRole.Cliente) clienteId = userId;
            }
            catch {  }

            if (!clienteId.HasValue)
                clienteId = HttpContext.Session.GetInt32("ClienteId");

            if (!clienteId.HasValue)
                return LocalRedirect(Url.Content("~/login"));

            Vehiculos = await _context.Vehiculos
                .AsNoTracking()
                .Where(v => v.IdCliente == clienteId.Value)
                .OrderBy(v => v.Marca).ThenBy(v => v.Modelo)
                .ToListAsync();

            return Page();
        }

        public static string CombustibleToText(byte c) => c switch
        {
            0 => "Gasolina",
            1 => "Diésel",
            2 => "Híbrido",
            3 => "Eléctrico",
            _ => "N/D"
        };

        public static string TransmisionToText(byte t) => t switch
        {
            0 => "Manual",
            1 => "Automática",
            2 => "CVT",
            _ => "N/D"
        };
    }
}