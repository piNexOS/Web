using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;


namespace Web.Pages.Gerenciamento
{
    [Authorize(Roles = "Gerente,Administrador,Programador")]
    public class Agentes : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public Agentes(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        public IList<Infra.DataBase.TabAgentes> TabAgentes { get; set; } = default!;
        public IList<Infra.DataBase.RoteiroDetalhes> TabRoteiroDetalhes { get; set; } = default!;
        public IList<Infra.DataBase.Roteiros> TabRoteiros { get; set; } = default!;

        public async Task OnGetAsync()
        {
            TabAgentes = await _context.TabAgentes
            .Include(a => a.Roteiros)
                .ThenInclude(r => r.RoteiroDetalhes)
                    .ThenInclude(d => d.IdOrdemServicoNavigation)
            .ToListAsync();
        }
    }
}

