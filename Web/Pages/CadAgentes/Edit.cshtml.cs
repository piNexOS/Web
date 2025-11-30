using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.CadAgentes
{
    [Authorize(Roles = "Gerente,Administrador")]
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

            // 1. Pega o agente atual do BD (com valor antigo)
            var agenteAtual = await _context.TabAgentes
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdTabAgente == TabAgentes.IdTabAgente);

            if (agenteAtual == null)
                return NotFound();

            // 2. Se a matrícula mudou, atualiza no usuário
            if (agenteAtual.Matricula != TabAgentes.Matricula)
            {
                var usuario = await _context.TabUsuarios
                    .FirstOrDefaultAsync(u => u.Matricula == agenteAtual.Matricula);

                if (usuario != null)
                {
                    usuario.Matricula = TabAgentes.Matricula;
                    _context.TabUsuarios.Update(usuario);
                }
            }

            // 3. Atualiza o agente
            
            TabAgentes.Status = "Inativo";
            _context.Attach(TabAgentes).State = EntityState.Modified;

            try
            {
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
