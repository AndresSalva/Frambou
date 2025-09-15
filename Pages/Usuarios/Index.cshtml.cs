using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalDeVehiculosUltimaVersion.Model;

namespace HospitalDeVehiculosUltimaVersion.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext _context;

        public IndexModel(HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext context)
        {
            _context = context;
        }

        public IList<Usuario> Usuario { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Usuario = await _context.Usuarios.ToListAsync();
        }
    }
}
