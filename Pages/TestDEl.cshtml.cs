using HospitalDeVehiculosUltimaVersion.Factory;
using HospitalDeVehiculosUltimaVersion.Factory.QrUltils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Buffers.Text;
using System.Data.SqlTypes;

namespace HospitalDeVehiculosUltimaVersion.Pages
{
    public class TestDElModel : PageModel
    {
        private readonly HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext _context;
        ServicioPagoDeQr servicioPagoDeQr;
        public string? qrBase64;

        public TestDElModel(HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext context)
        {
            _context = context;
            servicioPagoDeQr = new(_context, new BasicQrCodeGenerator());
        }
        public void OnGet()
        {
            SolicitudDePago solicitud = new(1, 10, "0");
            byte[] qrData = servicioPagoDeQr.CreateQrCode( solicitud );
            qrBase64 = Convert.ToBase64String(qrData);
            servicioPagoDeQr.ProcesarPago(solicitud);
        }
    }
}
