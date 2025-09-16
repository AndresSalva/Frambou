using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalDeVehiculosUltimaVersion.Pages.Servicios
{

    public class CreateModel : PageModel
    {
        private readonly HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext _context;

        public CreateModel(HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext context)
        {
            _context = context;
        }
        public int IdMantenimiento { get; set; }
        public IActionResult OnGet(int id)
        {
            IdMantenimiento = id;
            return Page();
        }

        [BindProperty]
        public Servicio Servicio { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Servicios.Add(Servicio);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
