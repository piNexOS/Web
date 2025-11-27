using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;
using Microsoft.AspNetCore.Authorization;


namespace Web.Pages.CadUsuarios
{
    [Authorize(Roles = "Gerente,Administrador")]
    public class IndexModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public IndexModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        public IList<TabUsuarios> TabUsuarios { get;set; } = default!;

        public async Task OnGetAsync()
        {
            TabUsuarios = await _context.TabUsuarios.ToListAsync();
        }
    }
}
