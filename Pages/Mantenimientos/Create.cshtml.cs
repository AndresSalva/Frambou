using System;
using System.Linq;
using System.Threading.Tasks;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Pages.Mantenimientos
{
    public class CreateModel : PageModel
    {
        private readonly HospitalDeVehiculosContext _context;

        public CreateModel(HospitalDeVehiculosContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mantenimiento Mantenimiento { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? idVehiculo { get; set; }

        public SelectList VehiculosOptions { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (idVehiculo.HasValue)
            {
                var exists = await _context.Vehiculos
                    .AsNoTracking()
                    .AnyAsync(v => v.Id == idVehiculo.Value);

                if (!exists)
                {
                    return RedirectToPage("./Index");
                }
            }
            else
            {
                // Sin id por query: cargar el dropdown
                VehiculosOptions = new SelectList(
                    await _context.Vehiculos
                        .AsNoTracking()
                        .OrderBy(v => v.Marca).ThenBy(v => v.Modelo)
                        .Select(v => new
                        {
                            v.Id,
                            Text = v.Placa + " — " + v.Marca + " " + v.Modelo
                        })
                        .ToListAsync(),
                    "Id", "Text");
            }

            // (Opcional) Sugerir una fecha inicial
            if (Mantenimiento.FechaProgramada == default)
                Mantenimiento.FechaProgramada = DateTime.Now.AddDays(1);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var adminId = HttpContext.Session.GetInt32("AdminProgId");
            if (adminId is null) return LocalRedirect(Url.Content("~/login"));

            Mantenimiento.IdAdminMantenimiento = adminId.Value;
            if (Mantenimiento.Estado == 0) Mantenimiento.Estado = 1;

            if ((Mantenimiento.IdVehiculo == 0 || Mantenimiento.IdVehiculo == default) && idVehiculo.HasValue)
                Mantenimiento.IdVehiculo = idVehiculo.Value;

            ModelState.Remove("Mantenimiento.IdAdminMantenimientoNavigation");
            ModelState.Remove("Mantenimiento.IdVehiculoNavigation");

            ModelState.Remove("Mantenimiento.IdAdminMantenimiento");
            ModelState.Remove("Mantenimiento.Estado");
            if (Mantenimiento.IdVehiculo != 0)
                ModelState.Remove("Mantenimiento.IdVehiculo");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Mantenimientos.Add(Mantenimiento);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
