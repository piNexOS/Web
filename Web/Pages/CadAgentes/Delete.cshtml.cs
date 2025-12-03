using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.CadAgentes
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
        public TabAgentes TabAgentes { get; set; } = default!;

        public TabUsuarios TabUsuarios { get; set; } = default!;

        

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabagentes = await _context.TabAgentes.FindAsync(id);
            if (tabagentes == null)
            {
                return NotFound();
            }

            var tabusuarios = await _context.TabUsuarios.FirstOrDefaultAsync(m => m.Matricula == tabagentes.Matricula);

            // 1️⃣ Atualizar todas as OS do agente
            var roteirosDoAgente = await _context.Roteiros
                .Include(r => r.RoteiroDetalhes)
                .ThenInclude(rd => rd.IdOrdemServicoNavigation)
                .Where(r => r.IdTabAgente == tabagentes.IdTabAgente)
                .ToListAsync();

            foreach (var roteiro in roteirosDoAgente)
            {
                foreach (var detalhe in roteiro.RoteiroDetalhes)
                {
                    if (detalhe.IdOrdemServicoNavigation != null)
                    {
                        detalhe.IdOrdemServicoNavigation.Status = "NÃO PROGRAMADA";
                    }
                }
            }

            // 2️⃣ Remover agente e usuário
            _context.TabAgentes.Remove(tabagentes);

            if (tabusuarios != null)
            {
                _context.TabUsuarios.Remove(tabusuarios);
            }

            // 3️⃣ Salvar tudo de uma vez
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
