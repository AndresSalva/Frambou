using HospitalDeVehiculosUltimaVersion.Factory;
using HospitalDeVehiculosUltimaVersion.Factory.FactoryRoll;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalDeVehiculosUltimaVersion.Factory.FactoryPago;
using HospitalDeVehiculosUltimaVersion.Factory.FactoryPago.QrUltils;
using Microsoft.Identity.Client;


namespace HospitalDeVehiculosUltimaVersion.Pages.Mantenimientos
{
    public class PagoQrModel : PageModel
    {
        private readonly HospitalDeVehiculosContext _context;
        private readonly ICurrentUserSession _session;
        private readonly ServicioPagoDeQr servicioPagoDeQr;
        private SolicitudDePago solicitudDePago;
        public string qrBase64 = string.Empty;

        [BindProperty]
        public Mantenimiento Mantenimiento { get; set; } = default!;

        public void OnGet()
        {
            decimal total = Mantenimiento.Servicios.Sum(s => s.Precio);
            solicitudDePago = new SolicitudDePago(Mantenimiento.IdVehiculoNavigation.IdCliente, total, "Bs");
            byte[] qrBytes = this.servicioPagoDeQr.CreateQrCode(solicitudDePago);
            qrBase64 = Convert.ToBase64String(qrBytes);
        }

        public PagoQrModel(ServicioPagoDeQr servicioPagoDeQr)
        {
            this.servicioPagoDeQr = servicioPagoDeQr;
            this.servicioPagoDeQr.SetQrCodeGenerator(new BasicQrCodeGenerator());
        }

        public void OnPost()
        {
            this.servicioPagoDeQr.ProcesarPago(solicitudDePago);
        }

    }
}
