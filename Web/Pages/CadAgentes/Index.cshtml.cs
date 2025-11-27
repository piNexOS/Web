using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.CadAgentes
{
    [Authorize(Roles = "Gerente,Administrador")]
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
