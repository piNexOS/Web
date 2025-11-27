using Infra.DataBase;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
namespace Web.Pages.NovaPasta
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        private readonly Infra.DataBase.STC_Context _context;

        public IndexModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }
        public class LoginViewModel
        {
            public string Matricula { get; set; }
            public string Senha { get; set; }
        }

        [BindProperty]
        public LoginViewModel Usuario { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var usuario = _context.TabUsuarios
                .FirstOrDefault(u => u.Matricula == Usuario.Matricula
                                  && u.Senha == Usuario.Senha);

            if (usuario != null)
            {
                // Login OK
                // gera cookie e autenticação de cargo
                // redireciona ou seta sessão

                await SignInUser(usuario, false);

                return RedirectToPage("/Home/Index");
            }
            else
            {
                ModelState.AddModelError("", "Matrícula ou senha inválida.");
                return Page();
            }
        }

        private async Task SignInUser(TabUsuarios user, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Matricula),
                new Claim(ClaimTypes.Role, user.Cargo) 
            };

            var identity = new ClaimsIdentity(claims, "Cookies");

            var props = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = rememberMe
                    ? DateTimeOffset.UtcNow.AddDays(5)
                    : DateTimeOffset.UtcNow.AddHours(1)
            };

            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(identity));
        }

    }
}
