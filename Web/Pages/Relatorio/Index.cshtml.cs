using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Relatorio
{
    [Authorize(Roles = "Administrador, Gerente, Programador")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
