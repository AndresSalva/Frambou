using System.Linq;
using System.Threading.Tasks;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Pages.Vehiculos
{
    public class CreateModel : PageModel
    {
        private readonly HospitalDeVehiculosContext _context;

        public CreateModel(HospitalDeVehiculosContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vehiculo Vehiculo { get; set; } = new();

        // Combo de clientes
        public SelectList ClientesOptions { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadClientesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Vehiculo.IdClienteNavigation");

            if (!ModelState.IsValid)
            {
                await LoadClientesAsync();
                return Page();
            }

            _context.Vehiculos.Add(Vehiculo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task LoadClientesAsync()
        {
            var items = await _context.Clientes
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c => new { c.Id, Texto = "Cliente #" + c.Id })
                .ToListAsync();

            ClientesOptions = new SelectList(items, "Id", "Texto");
        }
    }
}
