using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HospitalDeVehiculosUltimaVersion.Factory;

namespace HospitalDeVehiculosUltimaVersion.Pages
{
    public class TestDElModel : PageModel
    {
        private readonly HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext _context;
        ServicioPagoDeTarjeta servicioPagoDeTarjeta;

        public TestDElModel(HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext context)
        {
            _context = context;
            servicioPagoDeTarjeta = new(_context);
        }
        public void OnGet()
        {
            servicioPagoDeTarjeta.ProcesarPago(
                new SolicitudDePago(1, 10, "0")
            );
        }
    }
}
