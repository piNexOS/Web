using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Services
{
    public static class ProgServicos
    {
        public static void Programar(DateOnly pData, int pIdAgente, string[] pLista)
        {
            using (var ctx = new DataBase.STC_Context())
            {
                var roteiro = ctx.Roteiros.Where(x => x.Data == pData && x.IdTabAgente == pIdAgente).FirstOrDefault();
                if (roteiro == null)
                {
                    roteiro = new DataBase.Roteiros();
                    roteiro.Data = pData;
                    roteiro.IdTabAgente = pIdAgente;
                    ctx.Add(roteiro);
                    ctx.SaveChanges();
                }

                foreach (var item in pLista)
                {
                    var os = ctx.OrdensServicos.Where(x => x.IdOrdemServico == int.Parse(item) && x.Status == "" && x.DataBaixa == null).FirstOrDefault();
                    if (os == null) continue;

                    var det = new DataBase.RoteiroDetalhes();
                    det.IdRoteiro = roteiro.IdRoteiro;
                    det.IdOrdemServico = int.Parse(item);
                    det.Status = "";
                    ctx.Add(det);

                    os.Status = "PROGRAMADO";
                    ctx.SaveChanges();
                }
            }
        }

        public static void Transferir(DateOnly pData, int pIdAgente, string[] pLista)
        {
            using (var ctx = new DataBase.STC_Context())
            {

                var roteiro = ctx.Roteiros.Where(x => x.Data == pData && x.IdTabAgente == pIdAgente).FirstOrDefault();
                if (roteiro == null)
                {
                    roteiro = new DataBase.Roteiros();
                    roteiro.Data = pData;
                    roteiro.IdTabAgente = pIdAgente;
                    ctx.Add(roteiro);
                    ctx.SaveChanges();
                }
                var idRoteiroAnterior = ctx.RoteiroDetalhes.Where(x => x.IdRoteiroDetalhe == int.Parse(pLista[0])).FirstOrDefault().IdRoteiro;
                foreach (var item in pLista)
                {
                    var det = ctx.RoteiroDetalhes.Where(x => x.IdRoteiroDetalhe == int.Parse(item) && x.Status == "" && x.DataIniAtendimento == null).FirstOrDefault();
                    if (det == null) continue;
                    det.IdRoteiro = roteiro.IdRoteiro;
                    ctx.SaveChanges();
                }

                var det2 = ctx.RoteiroDetalhes.Where(x => x.IdRoteiro == idRoteiroAnterior).FirstOrDefault();
                if (det2 == null)
                {
                    var rot2 = ctx.Roteiros.Where(x => x.IdRoteiro == idRoteiroAnterior).FirstOrDefault();
                    ctx.Roteiros.Remove(rot2);
                    ctx.SaveChanges();
                }
            }
        }

        public static void Desprogramar(DateTime pData, int pIdAgente, string[] pLista)
        {
            int idOS, idRoteiro = 0;
            using (var ctx = new DataBase.STC_Context())
            {
                foreach (var item in pLista)
                {
                    var det = ctx.RoteiroDetalhes.Where(x => x.IdRoteiroDetalhe == int.Parse(item) && x.Status == "" && x.DataIniAtendimento == null).FirstOrDefault();
                    if (det == null) continue;
                    idOS = (int)det.IdOrdemServico;
                    idRoteiro = (int)det.IdRoteiro;
                    var os = ctx.OrdensServicos.Where(x => x.IdOrdemServico == idOS && x.Status == "PROGRAMADO").FirstOrDefault();
                    if (os != null)
                    {
                        os.Status = "";
                        ctx.OrdensServicos.Update(os);
                        ctx.RoteiroDetalhes.Remove(det);
                    }
                    ctx.SaveChanges();
                }

                var qtd = ctx.RoteiroDetalhes.Where(x => x.IdRoteiro == idRoteiro).Count();
                if (qtd == 0)
                {
                    var rot = ctx.Roteiros.Where(x => x.IdRoteiro == idRoteiro).FirstOrDefault();
                    ctx.Roteiros.Remove(rot);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
