using Infra.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


//Modelo de repositorio

namespace Infra.Repositories
{
    public class BairroRepository : RepositoryBase<DataBase.TabBairros>
    {
        DataBase.STC_Context ctx;

        public BairroRepository()
        {
            ctx = new DataBase.STC_Context();
        }
        public TabBairros GetByDescricao(string pDescricao)
        {
            return ctx.TabBairros.Where(x => x.Descricao == pDescricao).FirstOrDefault();
        }

        public SelectList GetSelectList(int IdMunicipio)
        {
            return new SelectList(ctx.TabBairros.AsNoTracking().Where(x => x.IdTabMunicipio == IdMunicipio).OrderBy(x => x.Descricao)
                .Select(s =>
                new SelectListItem
                {
                    Value = s.IdTabBairro.ToString(),
                    Text = s.Descricao
                }).ToList()
                , "Value", "Text");
        }
    }
}
