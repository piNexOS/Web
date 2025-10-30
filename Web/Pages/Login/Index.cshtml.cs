using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Infra.DataBase;
using System.Net.Mail;
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


        public void OnPost()
        {
            if(!ModelState.IsValid)
                return;
            
            var usuarios = _context.TabUsuarios.ToList();

            
        }
    }
}
