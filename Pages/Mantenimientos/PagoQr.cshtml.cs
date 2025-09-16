using System;
using System.Linq;
using System.Threading.Tasks;
using HospitalDeVehiculosUltimaVersion.Factory.FactoryPago;
using HospitalDeVehiculosUltimaVersion.Factory.FactoryPago.QrUltils;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Pages.Mantenimientos
{
    public class PagoQrModel : PageModel
    {
        private readonly HospitalDeVehiculosContext _context;
        private readonly ServicioPagoDeQr _servicioPagoDeQr;

        public PagoQrModel(HospitalDeVehiculosContext context, ServicioPagoDeQr servicioPagoDeQr)
        {
            _context = context;
            _servicioPagoDeQr = servicioPagoDeQr;
            _servicioPagoDeQr.SetQrCodeGenerator(new BasicQrCodeGenerator());
        }

        public Mantenimiento? Mantenimiento { get; private set; } = default!;
        public decimal Total { get; private set; }
        public string QrBase64 { get; private set; } = string.Empty;

        private SolicitudDePago _solicitudDePago = default!;
        [BindProperty]
        public int IdMantenimientoPost { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Mantenimiento = await _context.Mantenimientos
                .Include(m => m.Servicios)                  
                .Include(m => m.IdVehiculoNavigation)       
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Mantenimiento is null) return NotFound();

            Total = Mantenimiento.Servicios?.Sum(s => s.Precio) ?? 0m;

            if (Mantenimiento.IdVehiculoNavigation is null)
                return BadRequest("El mantenimiento no está vinculado a un vehículo válido.");

            var idCliente = Mantenimiento.IdVehiculoNavigation.IdCliente;

            _solicitudDePago = new SolicitudDePago(idCliente, Total, "Bs");

            byte[] qrBytes = _servicioPagoDeQr.CreateQrCode(_solicitudDePago);
            QrBase64 = Convert.ToBase64String(qrBytes);

            TempData["pago_total"] = Total.ToString();
            TempData["pago_moneda"] = "Bs";
            TempData["pago_cliente"] = idCliente.ToString();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var mantenimiento = await _context.Mantenimientos
                .Include(m => m.Servicios)
                .Include(m => m.IdVehiculoNavigation)
                .FirstOrDefaultAsync(m => m.Id == IdMantenimientoPost);

            if (mantenimiento is null) return NotFound();

            var total = mantenimiento.Servicios?.Sum(s => s.Precio) ?? 0m;
            var idCliente = mantenimiento.IdVehiculoNavigation?.IdCliente
                            ?? throw new InvalidOperationException("Vehículo/Cliente no disponible.");

            var solicitud = new SolicitudDePago(idCliente, total, "Bs");

            ResultadoDePago resultadoDePago = _servicioPagoDeQr.ProcesarPago(solicitud);
            if (resultadoDePago.Ok)
            {
                mantenimiento.Estado = 3;
                mantenimiento.FechaEjecucion = DateTime.Now;
                _context.Mantenimientos.Update(mantenimiento);
                await _context.SaveChangesAsync();  
            }

            return RedirectToPage("./Index"); 
        }
    }
}
