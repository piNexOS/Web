using Infra.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ServicoRepository : RepositoryBase<DataBase.TabServicos>
    {
        DataBase.STC_Context ctx;

        public ServicoRepository()
        {
            ctx = new DataBase.STC_Context();        
        }
        public TabServicos GetByDescricao(string pDescricao)
        {
            return ctx.TabServicos.Where(x => x.Descricao == pDescricao).FirstOrDefault();
        }

        public SelectList GetSelectList()
        {
            return new SelectList(ctx.TabServicos.AsNoTracking().OrderBy(x => x.Descricao)
                .Select(s =>
                new SelectListItem
                {
                    Value = s.IdTabServico.ToString(),
                    Text = s.Descricao
                }).ToList()
                , "Value", "Text");
        }
    }
}
