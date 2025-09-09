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
    public class DeleteModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public DeleteModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordensservicos = await _context.OrdensServicos.FindAsync(id);
            if (ordensservicos != null)
            {
                OrdensServicos = ordensservicos;
                _context.OrdensServicos.Remove(OrdensServicos);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
