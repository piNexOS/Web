using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class RoteiroRepository : RepositoryBase<DataBase.Roteiros>
    {
        DataBase.STC_Context ctx;
        public RoteiroRepository()
        {
            ctx = new DataBase.STC_Context();
        }
    }
}
