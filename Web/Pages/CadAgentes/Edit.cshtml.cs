using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;

namespace Web.Pages.CadAgentes
{
    public class EditModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public EditModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        [BindProperty]
        public TabAgentes TabAgentes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabagentes =  await _context.TabAgentes.FirstOrDefaultAsync(m => m.IdTabAgente == id);
            if (tabagentes == null)
            {
                return NotFound();
            }
            TabAgentes = tabagentes;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var agenteDb = await _context.TabAgentes
         .FirstOrDefaultAsync(a => a.IdTabAgente == TabAgentes.IdTabAgente);

            try
            {
                agenteDb.Nome = TabAgentes.Nome;
                agenteDb.Matricula = TabAgentes.Matricula;
                agenteDb.Cargo = TabAgentes.Cargo;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabAgentesExists(TabAgentes.IdTabAgente))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TabAgentesExists(int id)
        {
            return _context.TabAgentes.Any(e => e.IdTabAgente == id);
        }
    }
}
