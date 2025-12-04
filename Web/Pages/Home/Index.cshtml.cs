using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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
        public IList<Infra.DataBase.OrdensServicosRej> OrdensServicosRejs { get; set; } = default!;

        public int TotalRealizadas { get; set; }
        public int TotalPendentes { get; set; }
        public int TotalRejeitadas { get; set; }
        public int TotalNaoProgramadas { get; set; }

        public int PctRealizadas { get; set; }
        public int PctPendentes { get; set; }
        public int PctRejeitadas { get; set; }
        public int PctNaoProgramadas { get; set; }

        public List<string> Regioes { get; set; } = new();
        public List<int> QuantidadesRegioes { get; set; } = new();

        public string RegioesJSON => JsonSerializer.Serialize(Regioes);
        public string QuantidadesRegioesJSON => JsonSerializer.Serialize(QuantidadesRegioes);

        public async Task OnGetAsync()
        {
            var hoje = DateOnly.FromDateTime(DateTime.Today);

            // === AGENTES ===
            TabAgentes = await _context.TabAgentes
                .Include(a => a.Roteiros)
                    .ThenInclude(r => r.RoteiroDetalhes)
                        .ThenInclude(d => d.IdOrdemServicoNavigation)
                .Where(a =>
                    a.Roteiros.Any(r =>
                        r.RoteiroDetalhes.Any(d =>
                            d.IdOrdemServicoNavigation.Status != null &&
                            d.IdOrdemServicoNavigation.Status.ToUpper() == "PROGRAMADA"
                        )
                    )
                )
                .ToListAsync();

            // === ORDENS REJEITADAS ===
            OrdensServicosRejs = await _context.OrdensServicosRej
                .Include(o => o.IdRoteiroDetalhesNavigation)
                    .ThenInclude(rd => rd.IdOrdemServicoNavigation)
                        .ThenInclude(os => os.IdTabServicoNavigation)
                .Include(o => o.IdRoteiroDetalhesNavigation)
                    .ThenInclude(rd => rd.IdOrdemServicoNavigation)
                        .ThenInclude(os => os.IdTabBairroNavigation)
                .Include(o => o.IdRoteiroDetalhesNavigation)
                    .ThenInclude(rd => rd.IdRoteiroNavigation)
                        .ThenInclude(r => r.IdTabAgenteNavigation)
                .ToListAsync();

            // ============================================================
            // === QUANTIDADE DE OS POR STATUS (AJUSTADO AO SEU BANCO) ===
            // ============================================================

            // REJEITADAS
            TotalRejeitadas = await _context.OrdensServicosRej
                .CountAsync();

            // REALIZADAS NÃO EXISTEM NO SEU BANCO
            TotalRealizadas = 0;

            // PENDENTES = NULL ou ""
            TotalPendentes = await _context.OrdensServicos
                .CountAsync(o => o.Status == "PROGRAMADA");

            TotalNaoProgramadas = await _context.OrdensServicos
                .CountAsync(o => o.Status == "NÃO PROGRAMADA");

            // Cálculo dos percentuais
            int total = TotalRealizadas + TotalPendentes + TotalRejeitadas + TotalNaoProgramadas;

            PctRealizadas = total > 0 ? (int)Math.Round((double)TotalRealizadas / total * 100) : 0;
            PctPendentes = total > 0 ? (int)Math.Round((double)TotalPendentes / total * 100) : 0;
            PctRejeitadas = total > 0 ? (int)Math.Round((double)TotalRejeitadas / total * 100) : 0;
            PctNaoProgramadas = total > 0 ? (int)Math.Round((double)TotalNaoProgramadas / total * 100) : 0;


            // === GRÁFICO POR REGIÃO ===
            var regioesDB = await _context.OrdensServicos
                .Include(o => o.IdTabBairroNavigation)
                .GroupBy(o => o.IdTabBairroNavigation.Descricao)
                .Select(g => new { Nome = g.Key, Total = g.Count() })
                .ToListAsync();

            Regioes = regioesDB.Select(r => r.Nome).ToList();
            QuantidadesRegioes = regioesDB.Select(r => r.Total).ToList();
        }
    }
}
