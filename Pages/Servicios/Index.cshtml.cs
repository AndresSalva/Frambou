using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalDeVehiculosUltimaVersion.Model;

namespace HospitalDeVehiculosUltimaVersion.Pages.Servicios
{
    public class IndexModel : PageModel
    {
        private readonly HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext _context;

        public IndexModel(HospitalDeVehiculosUltimaVersion.Model.HospitalDeVehiculosContext context)
        {
            _context = context;
        }

        public IList<Servicio> Servicios { get;set; } = default!;
        public int IdMantenimiento { get; set; }

        public async Task OnGetAsync(int id)
        {
            IdMantenimiento = id;
            Servicios = await _context.Servicios
                .Where(s => s.IdMantenimiento == id).ToListAsync();
        }
    }
}
