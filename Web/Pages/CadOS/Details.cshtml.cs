using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;

namespace Web.Pages.CadOS
{
    public class DetailsModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public DetailsModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        public OrdensServicos OrdensServicos { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordensservicos = await _context.OrdensServicos.FirstOrDefaultAsync(m => m.IdOrdemServico == id);
            if (ordensservicos == null)
            {
                return NotFound();
            }
            else
            {
                OrdensServicos = ordensservicos;
            }
            return Page();
        }
    }
}
