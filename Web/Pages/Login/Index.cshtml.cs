using Infra.DataBase;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Login
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly STC_Context _context;

        public IndexModel(STC_Context context)
        {
            _context = context;
        }

        public void OnGet() { }

        public class LoginViewModel
        {
            public string Matricula { get; set; }
            public string Senha { get; set; }
        }

        [BindProperty]
        public LoginViewModel Usuario { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var usuario = _context.TabUsuarios
                .FirstOrDefault(u => u.Matricula == Usuario.Matricula
                                  && u.Senha == Usuario.Senha);

            if (usuario != null)
            {
                await SignInUser(usuario);
                return RedirectToPage("/Home/Index");
            }

            ModelState.AddModelError("", "Matrícula ou senha inválida.");
            return Page();
        }

        private async Task SignInUser(TabUsuarios user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Matricula),
                new Claim(ClaimTypes.Role, user.Cargo)
            };

            var identity = new ClaimsIdentity(claims, "Cookies");

            var props = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };

            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(identity), props);
        }

        // ---- handler de logout ----
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            // remove cookie do esquema "Cookies" (mesmo nome usado no SignIn)
            await HttpContext.SignOutAsync("Cookies");

        

            // redireciona para a página raiz (login)
            return Redirect("~/");
        }
    }
}
