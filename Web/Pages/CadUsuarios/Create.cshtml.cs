using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Infra.DataBase;

namespace Web.Pages.CadUsuarios
{
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
        public TabUsuarios TabUsuarios { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TabUsuarios.Add(TabUsuarios);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
