using Infra.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class OrdemServicoRejService
    {
        private readonly STC_Context _context;

        public OrdemServicoRejService(STC_Context context)
        {
            _context = context;
        }

    }
    
}