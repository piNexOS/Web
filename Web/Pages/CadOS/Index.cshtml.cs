using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;

namespace Web.Pages.CadOS
{
    public class IndexModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public IndexModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        public IList<OrdensServicos> OrdensServicos { get;set; } = default!;

        public async Task OnGetAsync()
        {
            OrdensServicos = await _context.OrdensServicos
                .Include(o => o.IdTabBairroNavigation)
                .Include(o => o.IdTabMunicipioNavigation)
                .Include(o => o.IdTabServicoNavigation).ToListAsync();
        }
    }
}
