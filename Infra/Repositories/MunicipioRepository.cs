using Infra.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infra.Repositories
{
    public class MunicipioRepository : RepositoryBase<DataBase.TabMunicipios>
    {
        DataBase.STC_Context ctx;

        public MunicipioRepository()
        {
            ctx = new DataBase.STC_Context();
        }
        public TabMunicipios GetByDescricao(string pDescricao)
        {
            return ctx.TabMunicipios.Where(x=>x.Descricao==pDescricao).FirstOrDefault();
        }

        public SelectList GetSelectList()
        {
            return new SelectList(ctx.TabMunicipios.AsNoTracking().OrderBy(x => x.Descricao)
                .Select(s =>
                new SelectListItem
                {
                    Value = s.IdTabMunicipio.ToString(),
                    Text = s.Descricao
                }).ToList()
                , "Value", "Text");
        }
    }
}