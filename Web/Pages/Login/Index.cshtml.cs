using Infra.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.NovaPasta
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public TabUsuarios Usuario { get; set; }
        public void OnGet()
        {
        }
    }
}
