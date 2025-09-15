using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalDeVehiculosUltimaVersion.Model;

namespace HospitalDeVehiculosUltimaVersion.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext _context;

        public IndexModel(HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext context)
        {
            _context = context;
        }

        public IList<Cliente> Cliente { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Cliente = await _context.Clientes
                .Include(c => c.IdNavigation).ToListAsync();
        }
    }
}
