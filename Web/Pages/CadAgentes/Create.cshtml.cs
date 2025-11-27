using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infra.DataBase;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.CadAgentes
{
    [Authorize(Roles = "Gerente,Administrador")]
    public class CreateModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public CreateModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TabAgentes TabAgentes { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TabAgentes.Status = "Inativo";
            _context.TabAgentes.Add(TabAgentes);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
