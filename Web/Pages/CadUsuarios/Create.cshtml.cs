using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infra.DataBase;
using Microsoft.AspNetCore.Authorization;


namespace Web.Pages.CadUsuarios
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
        public TabUsuarios TabUsuarios { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TabUsuarios.Add(TabUsuarios);

            if (TabUsuarios.Cargo == "Agente") {
                var agente = _context.TabAgentes.FirstOrDefault(u => u.Matricula == TabUsuarios.Matricula);
                
                if (agente == null) {
                    TabAgentes agenteEntity = new TabAgentes();
                    agenteEntity.Nome = TabUsuarios.Nome;
                    agenteEntity.Matricula = TabUsuarios.Matricula;
                    agenteEntity.Status = "Inativo";
                    
                    _context.TabAgentes.Add(agenteEntity);
                }
                
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
