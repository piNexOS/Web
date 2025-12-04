using Infra.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AndroidController : ControllerBase
    {
        private readonly STC_Context _context;
        public AndroidController(STC_Context context)
        {
            _context = context;
        }

        [HttpGet("usuarioById {id}")]
        public IActionResult GetById(string id)
        {
            var usuario = _context.TabUsuarios
                .Where(x => x.Matricula == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpGet("roteirosByMatriculaAgente {id}")]
        public IActionResult GetRoteiroById(int idAgente)
        {
            var roteiros = _context.Roteiros
                .Where(x => x.IdTabAgente == idAgente)
                .ToList();

            if (roteiros == null)
            {
                return NotFound();
            }
            return Ok(roteiros);
        }

        [HttpGet("roteiroDetalheById {id}")]
        public IActionResult GetRoteiroDetalheById(int idRoteiro)
        {
            var roteiroDetalhes = _context.RoteiroDetalhes
                .Where(x => x.IdRoteiro == idRoteiro)
                .ToList();
            if (roteiroDetalhes == null)
            {
                return NotFound();
            }
            return Ok(roteiroDetalhes);

        }

        [HttpPost("atualizaRoteiroDetalhe")]
        public IActionResult UpdateRoteiroDetalhe([FromBody] RoteiroDetalhes roteiroDetalhe)
        {
            var existingRoteiroDetalhe = _context.RoteiroDetalhes
                .FirstOrDefault(x => x.IdRoteiroDetalhe == roteiroDetalhe.IdRoteiroDetalhe);

            if (existingRoteiroDetalhe == null)
            {
                return NotFound();
            }

            existingRoteiroDetalhe.DataIniAtendimento = roteiroDetalhe.DataIniAtendimento;
            existingRoteiroDetalhe.DataFimAtendimento = roteiroDetalhe.DataFimAtendimento;
            existingRoteiroDetalhe.Status = roteiroDetalhe.Status;

            _context.SaveChanges();

            return Ok(existingRoteiroDetalhe);

        }

    }
}
