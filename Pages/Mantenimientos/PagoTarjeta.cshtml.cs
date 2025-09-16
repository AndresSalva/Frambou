using System.Linq;
using System.Threading.Tasks;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Pages.Mantenimientos
{
    public class PagoTarjetaModel : PageModel
    {
        private readonly HospitalDeVehiculosContext _context;

        public PagoTarjetaModel(HospitalDeVehiculosContext context)
        {
            _context = context;
        }

        public Mantenimiento Mantenimiento { get; private set; } = default!;
        public decimal Total { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Mantenimiento = await _context.Mantenimientos
                .Include(m => m.Servicios)
                .Include(m => m.IdVehiculoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Mantenimiento is null) return NotFound();

            Total = Mantenimiento.Servicios?.Sum(s => s.Precio) ?? 0m;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var m = await _context.Mantenimientos
                .Include(x => x.Servicios)
                .Include(x => x.IdVehiculoNavigation)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (m is null) return NotFound();

            var total = m.Servicios?.Sum(s => s.Precio) ?? 0m;

            // TODO: procesar pago con tarjeta aquí
            // - validar datos de tarjeta (si los agregas en el form)
            // - guardar registro de pago / estado
            // await _context.SaveChangesAsync();

            return RedirectToPage("./Detalle", new { id });
        }
    }
}
