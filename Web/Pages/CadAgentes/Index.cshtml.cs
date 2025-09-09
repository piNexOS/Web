using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;

namespace Web.Pages.CadAgentes
{
    public class IndexModel : PageModel
    {
        private readonly STC_Context _context;

        public IndexModel(STC_Context context)
        {
            _context = context;
        }

        public IList<TabAgentes> TabAgentes { get;set; } = default!;

        public async Task OnGetAsync()
        {
            TabAgentes = await _context.TabAgentes.ToListAsync();
        }
    }
}
