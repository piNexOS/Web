using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.CadUsuarios
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
        public TabUsuarios TabUsuarios { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabusuarios =  await _context.TabUsuarios.FirstOrDefaultAsync(m => m.IdTabUsuarios == id);
            if (tabusuarios == null)
            {
                return NotFound();
            }
            TabUsuarios = tabusuarios;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                var erros = new List<string>();

                foreach (var e in ModelState)
                {
                    foreach (var err in e.Value.Errors)
                    {
                        erros.Add($"{e.Key}: {err.ErrorMessage}");
                    }
                }

                throw new Exception(string.Join(" | ", erros));
            }

            var usuarioAtual = await _context.TabUsuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdTabUsuarios == TabUsuarios.IdTabUsuarios);

            if (usuarioAtual == null)
                return NotFound();

            // 2. Se a matrícula mudou, atualiza no agente
            if (usuarioAtual.Matricula != TabUsuarios.Matricula)
            {
                var agente = await _context.TabAgentes
                    .FirstOrDefaultAsync(u => u.Matricula == usuarioAtual.Matricula);

                if (agente != null)
                {
                    agente.Matricula = TabUsuarios.Matricula;
                    _context.TabAgentes.Update(agente);
                }
            }

            // 3. Atualiza o agente

            _context.TabUsuarios.Update(TabUsuarios);


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabUsuariosExists(TabUsuarios.IdTabUsuarios))
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

        private bool TabUsuariosExists(int id)
        {
            return _context.TabUsuarios.Any(e => e.IdTabUsuarios == id);
        }
    }
}
