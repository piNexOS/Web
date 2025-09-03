using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;

namespace Web.Pages.CadAgentes
{
    public class DetailsModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public DetailsModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        public TabAgentes TabAgentes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabagentes = await _context.TabAgentes.FirstOrDefaultAsync(m => m.IdTabAgente == id);
            if (tabagentes == null)
            {
                return NotFound();
            }
            else
            {
                TabAgentes = tabagentes;
            }
            return Page();
        }
    }
}
