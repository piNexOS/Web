using Infra.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            var ordensServico = ctx.OrdensServicos
            .Include(o => o.IdTabBairroNavigation)
                .ThenInclude(b => b.IdTabMunicipioNavigation)
            .Include(o => o.IdTabServicoNavigation)
            .Where(x => x.DataBaixa == null && x.Status == "NÃO PROGRAMADA");

            // Filtra por município, se fornecido
            if (idMunicipio != 0)
            {
                ordensServico = ordensServico.Where(x => x.IdTabMunicipio == idMunicipio);
            }

            if (idBairro != 0)
            {
                ordensServico = ordensServico.Where(x => x.IdTabBairro == idBairro);
            }

            if (idServico != 0) ordensServico = ordensServico.Where(x => x.IdTabServico == idServico);
            if (numCiclo != null) ordensServico = ordensServico.Where(x => x.Ciclo == numCiclo);
            if (dataVencimento != DateTime.MinValue) ordensServico = ordensServico.Where(x => x.DataLimite == dataVencimento)

            .Where(x => x.Status == "NÃO PROGRAMADA" && x.DataBaixa == null && x.IdTabBairroNavigation.IdTabMunicipio == idMunicipio);
            ordensServico = ordensServico.OrderBy(x => x.IdTabBairroNavigation.IdTabMunicipioNavigation.Descricao);
            return ordensServico.ToList();
        }

        public IList<DataBase.OrdensServicos> GetOrdensServicosParaGerenciar(
    int idMunicipio, int idBairro, int idServico, string status, DateTime? dataVencimento)
        {
            var query = ctx.OrdensServicos
                .Include(o => o.IdTabBairroNavigation)
                    .ThenInclude(b => b.IdTabMunicipioNavigation)
                .Include(o => o.IdTabServicoNavigation)
                .AsQueryable();

            // Filtra OS não baixadas
            query = query.Where(o => o.DataBaixa == null);

            // Filtra status, se informado
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }

            // Filtra município, se informado
            if (idMunicipio != 0)
            {
                query = query.Where(o => o.IdTabBairroNavigation.IdTabMunicipio == idMunicipio);
            }

            // Filtra bairro, se informado
            if (idBairro != 0)
            {
                query = query.Where(o => o.IdTabBairro == idBairro);
            }

            // Filtra serviço, se informado
            if (idServico != 0)
            {
                query = query.Where(o => o.IdTabServico == idServico);
            }

            // Filtra data de vencimento, se informada
            if (dataVencimento.HasValue && dataVencimento.Value != DateTime.MinValue)
            {
                query = query.Where(o => o.DataLimite.HasValue && o.DataLimite.Value.Date == dataVencimento.Value.Date);
            }

            // Ordena pelo nome do município
            query = query.OrderBy(o => o.IdTabBairroNavigation.IdTabMunicipioNavigation.Descricao);

            return query.ToList();
        }
    }
}
