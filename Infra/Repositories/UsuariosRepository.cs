using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class TabUsuariosRepository : RepositoryBase<DataBase.TabUsuarios>
    {
        private readonly DataBase.STC_Context ctx;

        public TabUsuariosRepository()
        {
            ctx = new DataBase.STC_Context();
        }

        public DataBase.TabUsuarios? GetByMatricula(string matricula)
        {
            return ctx.TabUsuarios.FirstOrDefault(u => u.Matricula == matricula);
        }
    }
}
