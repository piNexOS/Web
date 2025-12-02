using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Roteiros
{
    [Authorize(Roles = "Administrador, Gerente, Programador")]
    public class IndexModel : PageModel
    {
        public SelectList ListAgentes { get; set; }
        [BindProperty] public string[] Agente { get; set; }
        [BindProperty] public string Data { get; set; }
        [BindProperty] public string[] AgenteConfirma { get; set; }

        Infra.Repositories.AgenteRepository agenteRepository;
        Infra.Repositories.RoteiroDetalheRepository roteiroDetRepository;


        public IEnumerable<Infra.Repositories.RoteiroDetalheRepository.RoteiroDetModel> OrdensServico { get; set; }

        public IndexModel()
        {
            agenteRepository = new Infra.Repositories.AgenteRepository();
            roteiroDetRepository = new Infra.Repositories.RoteiroDetalheRepository();
            ListAgentes = agenteRepository.GetSelectList();
            OrdensServico = null;
        }

        public IActionResult OnGet()
        {

            ListAgentes = agenteRepository.GetSelectList();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Data == null) {
                OrdensServico = roteiroDetRepository.GetRoteiroDetAgente(Agente[0]);
                return Page(); 

            } else if (Agente[0] == null)
            {
                OrdensServico = roteiroDetRepository.GetRoteiroDetData(Data);
                return Page();
            }

            OrdensServico = roteiroDetRepository.GetRoteiroDet(Data, Agente[0]);

            return Page();
        }

    }
}
