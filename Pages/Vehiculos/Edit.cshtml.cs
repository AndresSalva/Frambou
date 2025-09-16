using System.Linq;
using System.Threading.Tasks;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Pages.Vehiculos
{
    public class EditModel : PageModel
    {
        private readonly HospitalDeVehiculosContext _context;
        public EditModel(HospitalDeVehiculosContext context) => _context = context;

        [BindProperty]
        public Vehiculo Vehiculo { get; set; } = new();
        public SelectList ClientesOptions { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null) return NotFound();

            var vehiculo = await _context.Vehiculos
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id.Value);

            if (vehiculo is null) return NotFound();

            Vehiculo = vehiculo;

            await LoadClientesAsync(Vehiculo.IdCliente);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Vehiculo.IdClienteNavigation");
            ModelState.Remove("Vehiculo.FechaRegistro");
            ModelState.Remove("Vehiculo.UltimaActualizacion");

            if (!ModelState.IsValid)
            {
                await LoadClientesAsync(Vehiculo?.IdCliente);
                return Page();
            }

            var db = await _context.Vehiculos.FirstOrDefaultAsync(v => v.Id == Vehiculo.Id);
            if (db is null) return NotFound();

            db.Marca = Vehiculo.Marca;
            db.Modelo = Vehiculo.Modelo;
            db.Placa = Vehiculo.Placa;
            db.Kilometraje = Vehiculo.Kilometraje;
            db.CapacidadMotor = Vehiculo.CapacidadMotor;
            db.Combustible = Vehiculo.Combustible;
            db.Transmision = Vehiculo.Transmision;
            db.IdCliente = Vehiculo.IdCliente;

            db.UltimaActualizacion = System.DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private async Task LoadClientesAsync(int? selectedId = null)
        {
            var items = await _context.Clientes
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c => new { c.Id, Texto = "Cliente #" + c.Id })
                .ToListAsync();

            ClientesOptions = new SelectList(items, "Id", "Texto", selectedId);
        }
    }
}
