using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;

namespace Web.Pages.CadUsuarios
{
    public class DetailsModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public DetailsModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        public TabUsuarios TabUsuarios { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabusuarios = await _context.TabUsuarios.FirstOrDefaultAsync(m => m.IdTabUsuarios == id);
            if (tabusuarios == null)
            {
                return NotFound();
            }
            else
            {
                TabUsuarios = tabusuarios;
            }
            return Page();
        }
    }
}
