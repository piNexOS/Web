using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.ProgServicos
{
    [Authorize(Roles = "Administrador, Gerente, Programador")]
    public class ProgServicosModel : PageModel
    {
        Infra.Repositories.MunicipioRepository municipioRepository;
        Infra.Repositories.BairroRepository bairroRepository;
        Infra.Repositories.AgenteRepository agenteRepository;
        Infra.Repositories.ServicoRepository servicoRepository;
        Infra.Repositories.OrdemServicoRepository ordemServicoRepository;
        public class PartialViewModelType
        {
            public IEnumerable<Infra.DataBase.OrdensServicos> OrdensServico { get; set; }
        }

        #region properties
        public SelectList ListAgentes { get; set; }
        public string AgenteSelected { get; set; }
        public SelectList ListServicos { get; set; }
        public string ServicoSelected { get; set; }
        public SelectList ListMotivos { get; set; }
        public string MotivoSelected { get; set; }
        public SelectList ListMunicipios { get; set; }
        public string MunicipioSelected { get; set; }
        public SelectList ListBairros { get; set; }
        public string BairroSelected { get; set; }
        public DateTime DataRoteiro { get; set; }
        public DateTime DataVencimento { get; set; }
        public string CPH { get; set; }
        public PartialViewModelType PartialViewModel { get; set; }

        #endregion
        public IActionResult OnGet()
        {
            //var perfil = HttpContext.Session.GetString("Perfil");
            //if (perfil != "ADMINISTRADOR")
                //return RedirectToPage("../Index");

            agenteRepository = new Infra.Repositories.AgenteRepository();
            ListAgentes = agenteRepository.GetSelectList();

            municipioRepository = new Infra.Repositories.MunicipioRepository();
            ListMunicipios = municipioRepository.GetSelectList();

            bairroRepository = new Infra.Repositories.BairroRepository();
            ListBairros = bairroRepository.GetSelectList(0);
            
            servicoRepository = new Infra.Repositories.ServicoRepository();
            ListServicos = servicoRepository.GetSelectList();
            
            
            return Page();
        }

        public JsonResult OnGetListBairros(int id)
        {
            bairroRepository = new Infra.Repositories.BairroRepository();
            return new JsonResult(bairroRepository.GetSelectList(id));
        }

        public PartialViewResult OnGetPartialView(int idMunicipio, int idBairro, int idServico, string numCiclo, DateTime dataVencimento)
        {
            ordemServicoRepository = new Infra.Repositories.OrdemServicoRepository();
            PartialViewModel = new PartialViewModelType();
            PartialViewModel.OrdensServico = ordemServicoRepository.GetOrdensServicosParaProgramar(idMunicipio, idBairro, idServico, numCiclo, dataVencimento);
            var ret = Partial("partialPage", PartialViewModel);
            return ret;
        }

        public JsonResult OnGetProgramar(string dataRoteiro, int idAgente, string idLista)
        {
            idLista = idLista.Substring(0, idLista.Count() - 1);
            var lista = idLista.Split(",");
            Infra.Services.ProgServicos.Programar(DateOnly.Parse(dataRoteiro), idAgente, lista);
            return new JsonResult(true);
        }
    }
}
