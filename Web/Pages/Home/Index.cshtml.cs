using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Home
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public IndexModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        public IList<Infra.DataBase.TabAgentes> TabAgentes { get; set; } = default!;
        public IList<Infra.DataBase.RoteiroDetalhes> TabRoteiroDetalhes { get; set; } = default!;
        public IList<Infra.DataBase.Roteiros> TabRoteiros { get; set; } = default!;

        public async Task OnGetAsync()
        {
            TabAgentes = await _context.TabAgentes
                .Where(a => a.Status == "Ativo")
                .Include(a => a.Roteiros)
                    .ThenInclude(r => r.RoteiroDetalhes)
                        .ThenInclude(d => d.IdOrdemServicoNavigation)
                .ToListAsync();
        }
    }
}
