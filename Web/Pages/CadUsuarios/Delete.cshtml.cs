using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.CadUsuarios
{
    [Authorize(Roles = "Gerente,Administrador")]
    public class DeleteModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public DeleteModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabusuarios = await _context.TabUsuarios.FindAsync(id);
            if (tabusuarios != null)
            {
                TabUsuarios = tabusuarios;
                _context.TabUsuarios.Remove(TabUsuarios);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
