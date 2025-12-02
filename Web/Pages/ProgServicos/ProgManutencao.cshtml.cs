using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.ProgServicos
{
    [Authorize(Roles = "Administrador, Gerente, Programador")]
    public class ProgManutencaoModel : PageModel
    {
        public SelectList ListAgentes { get; set; }
        [BindProperty] public string[] Agente { get; set; }
        [BindProperty] public string Data { get; set; }
        [BindProperty] public string[] AgenteConfirma { get; set; }

        Infra.Repositories.AgenteRepository agenteRepository;
        Infra.Repositories.RoteiroDetalheRepository roteiroDetRepository;

        
        public IEnumerable<Infra.Repositories.RoteiroDetalheRepository.RoteiroDetModel> OrdensServico { get; set; }
        public ProgManutencaoModel()
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
            if (Data == null || Agente[0] == null)
                return Page();

            OrdensServico = roteiroDetRepository.GetRoteiroDet(Data,Agente[0]);
            
            return Page();
        }

        public JsonResult OnGetDesprogramar(string pData, int pIdAgente, string pIdLista)
        {
            pIdLista = pIdLista.Substring(0, pIdLista.Count() - 1);
            var lista = pIdLista.Split(",");
            Infra.Services.ProgServicos.Desprogramar(DateTime.Parse(pData), pIdAgente, lista);
            return new JsonResult(true);
        }

        public JsonResult OnGetTransferir(string pData, int pIdAgente, string pIdLista)
        {
            pIdLista = pIdLista.Substring(0, pIdLista.Count() - 1);
            var lista = pIdLista.Split(",");
            Infra.Services.ProgServicos.Transferir(DateOnly.Parse(pData), pIdAgente, lista);
            return new JsonResult(true);
        }

    }
}
