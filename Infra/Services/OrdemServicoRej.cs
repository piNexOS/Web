using Infra.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infra.Services
{
    public class OrdemServicoRejService
    {
        private readonly STC_Context _context;

        public OrdemServicoRejService(STC_Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Rejeita uma ordem de serviço criando um registro em OrdensServicosRej
        /// </summary>
        public async Task<(bool Sucesso, string Mensagem)> RejeitarOrdemAsync(int idOrdemServico, string motivo)
        {
            // 1. Verifica se existe OS
            var os = await _context.OrdensServicos
                .FirstOrDefaultAsync(o => o.IdOrdemServico == idOrdemServico);


            if (os == null)
                return (false, "Ordem de Serviço não encontrada.");

            // 2. (Opcional) Verifica se já está rejeitada
            var jaRejeitada = await _context.OrdensServicosRej
                .AnyAsync(r => r.IdOrdemServico == idOrdemServico);

            if (jaRejeitada)
                return (false, "Essa ordem de serviço já foi rejeitada.");

            // 3. Cria o registro de rejeição
            var rejeicao = new OrdensServicosRej
            {
                IdOrdemServico = idOrdemServico,
                Motivo = motivo,
            };

            os.Status = "Rejeitada";
            os.DataBaixa = DateTime.Now;
            _context.OrdensServicosRej.Add(rejeicao);          

            await _context.SaveChangesAsync();

            return (true, "Ordem rejeitada com sucesso.");
        }
    }
    
}