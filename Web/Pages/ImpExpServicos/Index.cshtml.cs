using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Immutable;

namespace Web.Pages.ImpExpServicos
{
    public class IndexModel : PageModel
    {
        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public List<IFormFile> ListFiles { get; set; }


        public IndexModel(IWebHostEnvironment environment)
        {
            _webHostEnvironment = environment;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public void OnPostUpload()
        {
            string fileName;
            List<string> listFiles = new List<string>();
            string path = _webHostEnvironment.ContentRootPath + "\\temp\\";
            foreach (var item in ListFiles)
            {
                fileName = path + item.FileName;
                listFiles.Add(fileName);
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    item.CopyTo(fileStream);
                }
            }
            var ImpExp = new Infra.Services.ImpExpServicos();
            ImpExp.Importar(listFiles);
        }
    }
}
