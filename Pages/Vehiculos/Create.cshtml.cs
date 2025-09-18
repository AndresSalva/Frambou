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

        public enum TipoTransmision : byte
        {
            Manual = 0,
            Automatica = 1,
            CVT = 2
        }

        public enum TipoCombustible : byte
        {
            Gasolina = 0,
            Diesel = 1,
            Hibrido = 2,
            Electrico = 3
        }

        private readonly HospitalDeVehiculosContext _context;

        public CreateModel(HospitalDeVehiculosContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vehiculo Vehiculo { get; set; } = new();

        // Combo de clientes
        public SelectList ClientesOptions { get; private set; } = default!;
        public SelectList TransmisionOptions { get; private set; } = default!;
        public SelectList CombustibleOptions { get; private set; } = default!;
        public SelectList CapacidadMotorOptions { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadClientesAsync();
            LoadTransmisionOptions();
            LoadCombustibleOptions();
            LoadCapacidadMotorOptions();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Vehiculo.IdClienteNavigation"); // si existe la nav

            if (!ModelState.IsValid)
            {
                await LoadClientesAsync();
                LoadTransmisionOptions();
                LoadCombustibleOptions();
                LoadCapacidadMotorOptions();
                return Page();
            }

            _context.Vehiculos.Add(Vehiculo);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private async Task LoadClientesAsync()
        {
            var items = await _context.Clientes.Include(c => c.IdNavigation)
                .AsNoTracking()
                .Select(c => new { c.Id, Texto = $"{ c.IdNavigation.PrimerNombre } {c.IdNavigation.PrimerApellido}"})
                .ToListAsync();

            ClientesOptions = new SelectList(items, "Id", "Texto");
        }

        private void LoadTransmisionOptions()
        {
            var items = Enum.GetValues(typeof(TipoTransmision))
                .Cast<TipoTransmision>()
                .Select(e => new { Id = (byte)e, Nombre = e == TipoTransmision.Automatica ? "Automática" : e.ToString() })
                .ToList();

            TransmisionOptions = new SelectList(items, "Id", "Nombre");
        }

        private void LoadCombustibleOptions()
        {
            var items = Enum.GetValues(typeof(TipoCombustible))
                .Cast<TipoCombustible>()
                .Select(e => new { Id = (byte)e, Nombre = e.ToString() })
                .ToList();

            CombustibleOptions = new SelectList(items, "Id", "Nombre");
        }

        private void LoadCapacidadMotorOptions()
        {
            var items = new[] { 1, 2, 3, 4 }
                .Select(n => new { Id = n, Nombre = n.ToString() })
                .ToList();

            CapacidadMotorOptions = new SelectList(items, "Id", "Nombre");
        }

    }
}
