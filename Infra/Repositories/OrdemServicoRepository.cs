using Infra.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class OrdemServicoRepository : RepositoryBase<DataBase.OrdensServicos>
    {
        DataBase.STC_Context ctx;

        public OrdemServicoRepository()
        {
            ctx = new DataBase.STC_Context();
        } 

        public OrdensServicos GetByNumOS(string pNumOS)
        {
            return ctx.OrdensServicos.Where(x => x.NumeroOS == pNumOS).FirstOrDefault();
        }

        public IList<DataBase.OrdensServicos> GetOrdensServicosParaProgramar(int idMunicipio, int idBairro, int idServico, string numCiclo, DateTime? dataVencimento)
        {
            var OrdensServico = ctx.OrdensServicos
                .Include(o => o.IdTabBairroNavigation)
                //.include(o => o.IdTabBairroNavigation.IdTabMunicipioNavigation)
                .Include(o => o.IdTabServicoNavigation)
                .Where(x => x.DataBaixa == null && x.Status == "" && x.IdTabMunicipio == idMunicipio);

            if (idBairro != 0) OrdensServico = OrdensServico.Where(x => x.IdTabBairro == idBairro);
            if (idServico != 0) OrdensServico = OrdensServico.Where(x => x.IdTabServico == idServico);
            if (numCiclo != null) OrdensServico = OrdensServico.Where(x => x.Ciclo == numCiclo);
            if (dataVencimento != DateTime.MinValue) OrdensServico = OrdensServico.Where(x => x.DataLimite == dataVencimento);

            //.Where(x => x.Status == "" && x.DataBaixa == null && x.IdBairroNavigation.IdMunicipio == idMunicipio);
            OrdensServico = OrdensServico.OrderBy(x => x.IdTabBairroNavigation.IdTabMunicipioNavigation.Descricao);
            return OrdensServico.ToList();
        }
    }
}
