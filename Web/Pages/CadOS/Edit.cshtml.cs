using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;

namespace Web.Pages.CadOS
{
    public class EditModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public EditModel(Infra.DataBase.STC_Context context)
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

            var ordensservicos =  await _context.OrdensServicos.FirstOrDefaultAsync(m => m.IdOrdemServico == id);
            if (ordensservicos == null)
            {
                return NotFound();
            }
            OrdensServicos = ordensservicos;
           ViewData["IdTabBairro"] = new SelectList(_context.TabBairros, "IdTabBairro", "Descricao");
           ViewData["IdTabMunicipio"] = new SelectList(_context.TabMunicipios, "IdTabMunicipio", "Descricao");
           ViewData["IdTabServico"] = new SelectList(_context.TabServicos, "IdTabServico", "Descricao");
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

            _context.Attach(OrdensServicos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdensServicosExists(OrdensServicos.IdOrdemServico))
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

        private bool OrdensServicosExists(int id)
        {
            return _context.OrdensServicos.Any(e => e.IdOrdemServico == id);
        }
    }
}
