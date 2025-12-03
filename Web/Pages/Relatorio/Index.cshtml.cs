using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Infra.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Web.Pages.Relatorio
{
    [Authorize(Roles = "Administrador, Gerente, Programador")]
    public class IndexModel(STC_Context context) : PageModel
    {
        [BindProperty]
        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1);

        [BindProperty]
        public DateTime EndDate { get; set; } = DateTime.Now;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var roteiroDetalhes = await context.RoteiroDetalhes
                .Where(rd => rd.DataFimAtendimento >= StartDate &&
                             rd.DataFimAtendimento <= EndDate.AddDays(1) &&
                             rd.Status == "Realizada")
                .AsNoTracking()
                .ToListAsync();

            var builder = new StringBuilder();

            builder.AppendLine("IdRoteiroDetalhe,IdRoteiro,IdOrdemServico,DataIniAtendimento,DataFimAtendimento,Status,Latitude,Longitude,HoraDownload,HoraUpload");

            foreach (var rd in roteiroDetalhes)
            {
                builder.AppendLine($"{rd.IdRoteiroDetalhe},{rd.IdRoteiro},{rd.IdOrdemServico},{rd.DataIniAtendimento},{rd.DataFimAtendimento},{rd.Status},{rd.Latitude},{rd.Longitude},{rd.HoraDownload},{rd.HoraUpload}");
            }

            string fileName = $"Relatorio_RoteiroDetalhes_{DateTime.Now:yyyyMMddHHmmss}.csv";
            byte[] fileBytes = Encoding.UTF8.GetBytes(builder.ToString());

            return File(fileBytes, "text/csv", fileName);
        }
    }
}
