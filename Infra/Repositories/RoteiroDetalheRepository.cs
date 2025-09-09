using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class RoteiroDetalheRepository : RepositoryBase<DataBase.RoteiroDetalhes>
    {
        public class RoteiroDetModel
        {
            public int IdRoteiroDet { get; set; }
            public int IdOS { get; set; }
            public string NumOS { get; set; }
            public string Matricula { get; set; }
            public string NumHidrometro { get; set; }
            public DateTime? DataVencimento { get; set; }
            public string Ciclo { get; set; }
            public string Servico { get; set; }
            public string Status { get; set; }
            public DateTime? DataIniAtendimento { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
        }

        DataBase.STC_Context ctx;

        public RoteiroDetalheRepository() 
        {
            ctx = new DataBase.STC_Context();
        }

        public IEnumerable<RoteiroDetModel> GetRoteiroDet(string pDataRoteiro, string pIdAgente)
        {
            using (ctx)
            {
                var result = (from rd in ctx.RoteiroDetalhes
                              join ro in ctx.Roteiros on rd.IdRoteiro equals ro.IdRoteiro
                              join os in ctx.OrdensServicos on rd.IdOrdemServico equals os.IdOrdemServico
                              join ba in ctx.TabBairros on os.IdTabBairro equals ba.IdTabBairro
                              join mu in ctx.TabMunicipios on ba.IdTabMunicipio equals mu.IdTabMunicipio
                              where ro.Data == DateOnly.Parse(pDataRoteiro) && ro.IdTabAgente == int.Parse(pIdAgente)
                              orderby rd.DataIniAtendimento descending
                              select new RoteiroDetModel
                              {
                                  IdRoteiroDet = rd.IdRoteiroDetalhe,
                                  IdOS = os.IdOrdemServico,
                                  Matricula = os.Matricula.ToString(),
                                  NumHidrometro = os.NumHD,
                                  DataVencimento = os.DataLimite,
                                  Ciclo = os.Ciclo,
                                  Bairro = ba.Descricao,
                                  Cidade = mu.Descricao,
                                  Servico = os.IdTabServicoNavigation.Descricao,
                                  NumOS = os.NumeroOS,
                                  Status = os.Status,
                                  DataIniAtendimento = rd.DataIniAtendimento
                              }).ToList();

                return result;
            }
        }
    }
}
