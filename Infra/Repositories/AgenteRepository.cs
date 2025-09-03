using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infra.Repositories
{
    public class AgenteRepository : RepositoryBase<DataBase.TabAgentes>
    {
        DataBase.STC_Context ctx;

        public AgenteRepository() 
        {
            ctx = new DataBase.STC_Context();
        }

        public SelectList GetSelectList()
        {
            return new SelectList(ctx.TabAgentes.AsNoTracking().OrderBy(x => x.Nome)
                .Select(s =>
                new SelectListItem
                {
                    Value = s.IdTabAgente.ToString(),
                    Text = s.Nome
                }).ToList()
                , "Value", "Text");
        }
    }
}
