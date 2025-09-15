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
        private readonly ServicioPagoDeQr _qrService;
        public string? qrBase64;

        public TestDElModel(ServicioPagoDeQr servicioPagoDeQr)
        {
            _qrService = servicioPagoDeQr;
            servicioPagoDeQr.SetQrCodeGenerator(new BasicQrCodeGenerator());
        }
        public void OnGet()
        {
            SolicitudDePago solicitud = new(1, 10, "0");
            byte[] qrData = _qrService.CreateQrCode( solicitud );
            qrBase64 = Convert.ToBase64String(qrData);
            _qrService.ProcesarPago(solicitud);
        }
    }
}
