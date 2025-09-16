using System.Linq;
using System.Threading.Tasks;
using HospitalDeVehiculosUltimaVersion.Factory.FactoryPago;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Pages.Mantenimientos
{
    public class PagoTarjetaModel : PageModel
    {
        private readonly HospitalDeVehiculosContext _context;
        private readonly ServicioPagoDeTarjeta _servicioDeTarjeta;
        [BindProperty]
        public string CVV
        {
            get; set;
        }
        [BindProperty]
        public string VENCIMIENTO
        {
            get; set;
        }
        [BindProperty]
        public string NUMERO_TARJETA
        {
            get; set;
        }
        [BindProperty]
        public int IdMantenimientoPost { get; set; }


        public PagoTarjetaModel(HospitalDeVehiculosContext context, ServicioPagoDeTarjeta servicioDeTarjeta)
        {
            _context = context;
            _servicioDeTarjeta = servicioDeTarjeta;
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

        public async Task<IActionResult> OnPostAsync()
        {
            Mantenimiento? mantenimiento = await _context.Mantenimientos
                .Include(x => x.Servicios)
                .FirstOrDefaultAsync(x => x.Id == IdMantenimientoPost);

            if (mantenimiento is null) return NotFound();

            var total = mantenimiento.Servicios?.Sum(s => s.Precio) ?? 0m;
            Vehiculo? vehiculo = await _context.Vehiculos.FirstOrDefaultAsync(v => v.Id == mantenimiento.IdVehiculo);
            if (vehiculo is null) return NotFound();
            SolicitudDePago solicitudDePago = new() { 
                Divisa = "BS",
                IdCliente = vehiculo.IdCliente,
                Total = total,
                MetaDatos = { { MetaDato.CVV, CVV },{ MetaDato.NUMERO_TARJETA, NUMERO_TARJETA }, {  MetaDato.FECHA_VENCIMIENTO, VENCIMIENTO }   }
            };

            solicitudDePago.IdCliente = vehiculo.IdCliente;

            ResultadoDePago resultadoDePago = _servicioDeTarjeta.ProcesarPago(solicitudDePago);
            if(resultadoDePago.Ok != false)
            {
                mantenimiento.Estado = 3;
                mantenimiento.FechaEjecucion = DateTime.Now;
                _context.Mantenimientos.Update(mantenimiento);

                await _context.SaveChangesAsync();
            }


            // TODO: procesar pago con tarjeta aquí
            // - validar datos de tarjeta (si los agregas en el form)
            // - guardar registro de pago / estado
            // await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
