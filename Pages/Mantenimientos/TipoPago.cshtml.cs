using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalDeVehiculosUltimaVersion.Pages.Mantenimientos
{
    public class TipoPagoModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; } 

        public void OnGet()
        {
        }
    }
}
