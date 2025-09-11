using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infra.DataBase;

namespace Web.Pages.CadOS
{
    public class EditModel : PageModel
    {
        private readonly Infra.DataBase.STC_Context _context;

        public EditModel(Infra.DataBase.STC_Context context)
        {
            _context = context;
        }

        [BindProperty]
        public OrdensServicos OrdensServicos { get; set; } = default!;

        // Campos de texto que o usuário vai digitar
        [BindProperty]
        public string? ServicoDigitado { get; set; }

        [BindProperty]
        public string? BairroDigitado { get; set; }

        [BindProperty]
        public string? MunicipioDigitado { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordensservicos = await _context.OrdensServicos
                .FirstOrDefaultAsync(m => m.IdOrdemServico == id);

            if (ordensservicos == null)
            {
                return NotFound();
            }

            OrdensServicos = ordensservicos;

            // Pré-carregar os valores atuais nos campos de texto
            ServicoDigitado = OrdensServicos.IdTabServico.ToString();
            BairroDigitado = OrdensServicos.IdTabBairro.ToString();
            MunicipioDigitado = OrdensServicos.IdTabMunicipio.ToString();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (string.IsNullOrWhiteSpace(ServicoDigitado))
            {
                ModelState.AddModelError("ServicoDigitado", "O campo Serviço é obrigatório.");
                return Page();
            }

            // Normaliza o texto (evita duplicação por maiúsculas ou espaços)
            var nomeServico = ServicoDigitado.Trim().ToUpper();

            // Verifica se o serviço já existe no banco
            var servico = await _context.TabServicos
                .FirstOrDefaultAsync(s => s.Descricao.ToLower() == nomeServico);

            if (servico == null)
            {
                // Se não existe, cria novo serviço

                servico = new Infra.DataBase.TabServicos

                {
                    Descricao = ServicoDigitado.Trim()
                };

                _context.TabServicos.Add(servico);
                await _context.SaveChangesAsync(); // Salva agora para obter o ID
            }

            // Atribui o ID do serviço (existente ou recém-criado)
            OrdensServicos.IdTabServico = servico.IdTabServico;

            // Configuração do Município
            //---------------------------------------------------------------------------

            if (string.IsNullOrWhiteSpace(MunicipioDigitado))
            {
                ModelState.AddModelError("Municipio Digitado", "O campo Serviço é obrigatório.");
                return Page();
            }

            // Normaliza o texto (evita duplicação por maiúsculas ou espaços)
            var nomeMunicipio = MunicipioDigitado.Trim().ToUpper();

            // Verifica se o serviço já existe no banco
            var municipio = await _context.TabMunicipios
                .FirstOrDefaultAsync(s => s.Descricao.ToLower() == nomeMunicipio);

            if (municipio == null)
            {
                // Se não existe, cria novo serviço

                municipio = new Infra.DataBase.TabMunicipios

                {
                    Descricao = MunicipioDigitado.Trim()
                };

                _context.TabMunicipios.Add(municipio);
                await _context.SaveChangesAsync(); // Salva agora para obter o ID
            }

            // Atribui o ID do serviço (existente ou recém-criado)
            OrdensServicos.IdTabMunicipio = municipio.IdTabMunicipio;

            // Configuração do Bairro
            //---------------------------------------------------------------------------

            if (string.IsNullOrWhiteSpace(BairroDigitado))
            {
                ModelState.AddModelError("Bairro Digitado", "O campo Bairro é obrigatório.");
                return Page();
            }

            // Normaliza o texto (evita duplicação por maiúsculas ou espaços)
            var nomeBairro = BairroDigitado.Trim().ToUpper();

            // Verifica se o bairro já existe no banco
            var bairro = await _context.TabBairros
                .FirstOrDefaultAsync(s => s.Descricao.ToLower() == nomeBairro);

            if (bairro == null)
            {
                // Se não existe, cria novo bairro

                bairro = new Infra.DataBase.TabBairros

                {
                    Descricao = BairroDigitado.Trim(),
                    IdTabMunicipio = municipio.IdTabMunicipio
                };

                _context.TabBairros.Add(bairro);
                await _context.SaveChangesAsync(); // Salva agora para obter o ID
            }
            OrdensServicos.IdTabBairro = bairro.IdTabBairro;



            _context.OrdensServicos.Update(OrdensServicos);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }
    }
}
