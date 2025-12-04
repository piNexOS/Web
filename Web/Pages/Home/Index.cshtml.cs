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
        public IList<Infra.DataBase.OrdensServicos> OrdemServicos = default!;
        public IList<Infra.DataBase.OrdensServicosRej> OrdensServicosRejs = default!;



        public async Task OnGetAsync()
        {
            TabAgentes = await _context.TabAgentes
                .Include(a => a.Roteiros)
                    .ThenInclude(r => r.RoteiroDetalhes)
                        .ThenInclude(d => d.IdOrdemServicoNavigation)
                .Where(a =>
                    a.Roteiros.Any(r =>
                        r.RoteiroDetalhes.Any(d =>
                            d.IdOrdemServicoNavigation.Status == "PROGRAMADA"
                        )
                    )
                )
                .ToListAsync();

            // CORREÇÃO: carregar todos os navigations necessários
            OrdensServicosRejs = await _context.OrdensServicosRej
                .Include(o => o.IdRoteiroDetalhesNavigation)
                    .ThenInclude(rd => rd.IdOrdemServicoNavigation)
                        .ThenInclude(os => os.IdTabServicoNavigation)

                .Include(o => o.IdRoteiroDetalhesNavigation)
                    .ThenInclude(rd => rd.IdOrdemServicoNavigation)
                        .ThenInclude(os => os.IdTabBairroNavigation)

                .Include(o => o.IdRoteiroDetalhesNavigation)
                    .ThenInclude(rd => rd.IdOrdemServicoNavigation)

                .Include(o => o.IdRoteiroDetalhesNavigation)
                    .ThenInclude(rd => rd.IdRoteiroNavigation)
                        .ThenInclude(r => r.IdTabAgenteNavigation)

                .ToListAsync();
        }
    }
}
