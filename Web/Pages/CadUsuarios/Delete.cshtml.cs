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

        public TabAgentes TabAgentes { get; set; }

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

            // 1️⃣ Buscar usuário
            var tabusuarios = await _context.TabUsuarios.FindAsync(id);
            if (tabusuarios == null)
                return NotFound();

            TabUsuarios = tabusuarios;

            // 2️⃣ Verificar se existe um agente com a mesma matrícula
            var agente = await _context.TabAgentes
                .FirstOrDefaultAsync(a => a.Matricula == tabusuarios.Matricula);

            if (agente != null)
            {
                // 3️⃣ Atualizar todas as OS programadas do agente
                var roteirosDoAgente = await _context.Roteiros
                    .Include(r => r.RoteiroDetalhes)
                    .ThenInclude(rd => rd.IdOrdemServicoNavigation)
                    .Where(r => r.IdTabAgente == agente.IdTabAgente)
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

                // 4️⃣ Remover o agente
                _context.TabAgentes.Remove(agente);
            }

            // 5️⃣ Remover o usuário
            _context.TabUsuarios.Remove(tabusuarios);

            // 6️⃣ Salvar tudo
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}