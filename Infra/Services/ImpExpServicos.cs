using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Services
{
    public class ImpExpServicos
    {
        Repositories.OrdemServicoRepository repOrdemServico = new Infra.Repositories.OrdemServicoRepository();
        Repositories.MunicipioRepository repMunicipio = new Infra.Repositories.MunicipioRepository();
        Repositories.BairroRepository repBairro = new Infra.Repositories.BairroRepository();
        Repositories.ServicoRepository repServico = new Infra.Repositories.ServicoRepository();
        public ImpExpServicos()
        {
            
        }
        public void Importar(List<string> pListFiles)
        {
            var stringConnection = "Provider=Microsoft.Ace.OleDb.12.0;data source=" + pListFiles[0] + ";Extended Properties=Excel 12.0;";
            var sql = "SELECT * FROM [ImpressaoSsIdRecepcao$]";

            var conn = new OleDbConnection();
            conn.ConnectionString = stringConnection;
            conn.Open();

            var dataAdapter = new OleDbDataAdapter(sql, conn);
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "servicos");

            string dcrBairro, dcrServico, dcrMunicipio, numOS, codServico;

            using (var ctx = new DataBase.STC_Context())
            {
                //foreach (DataRow item in dataSet.Tables[0].Rows)
                for (int i = 2; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var item=dataSet.Tables[0].Rows[i];

                    if (item[0].ToString() == "") break;

                    numOS = item[0].ToString();
                    dcrBairro = item[10].ToString();
                    dcrMunicipio = item[9].ToString();
                    dcrServico = item[4].ToString();
                    codServico = item[3].ToString();

                    var servico = repServico.GetByDescricao(dcrServico);
                    if (servico == null)
                    {
                        servico = new DataBase.TabServicos();
                        servico.Descricao = dcrServico;
                        servico.CodServico = codServico;
                        repServico.Add(servico);
                    }

                    var municipio = repMunicipio.GetByDescricao(dcrMunicipio);
                    if (municipio == null)
                    {
                        municipio = new DataBase.TabMunicipios();
                        municipio.Descricao = dcrMunicipio;
                        repMunicipio.Add(municipio);
                    }

                    var bairro = repBairro.GetByDescricao(dcrBairro);
                    if(bairro == null)
                    {
                        bairro = new DataBase.TabBairros();
                        bairro.Descricao = dcrBairro;
                        bairro.IdTabMunicipio = repMunicipio.GetByDescricao(dcrMunicipio).IdTabMunicipio;
                        repBairro.Add(bairro);
                    }

                    var ordemServico = repOrdemServico.GetByNumOS(numOS);
                    if (ordemServico == null)
                    {
                        ordemServico = new DataBase.OrdensServicos();
                        ordemServico.NumeroOS = numOS;
                        ordemServico.CPF = item[18].ToString();
                        ordemServico.CNPJ = item[19].ToString();
                        ordemServico.UltimaLeitura = item[22].ToString();

                        //Resolver Data Limite de acordo com serviço
                        ordemServico.DataLimite = DateTime.Now.Date.AddDays(7);

                        ordemServico.NumHD = item[21].ToString();
                        ordemServico.NumeroCasa = item[12].ToString();
                        ordemServico.Ciclo = item[25].ToString();
                        ordemServico.Classificacao = item[26].ToString();
                        ordemServico.Complemento = item[13].ToString();
                        ordemServico.DataImportacao = DateTime.Now;
                        ordemServico.LocalizacaoPadrao = item[24].ToString();
                        ordemServico.TipoPadrao = item[23].ToString();
                        ordemServico.DataRecepcao = DateTime.Parse(item[28].ToString());
                        ordemServico.DataRegistro = DateTime.Parse(item[27].ToString());
                        ordemServico.Endereco = item[11].ToString();
                        ordemServico.Matricula = item[2].ToString();
                        ordemServico.Informacoes = item[5].ToString();
                        ordemServico.Referencia = item[7].ToString();
                        ordemServico.TelCliente = item[20].ToString();
                        ordemServico.TelSolicitante = item[15].ToString();
                        ordemServico.NomeCliente = item[17].ToString();
                        ordemServico.NomeSolicitante = item[15].ToString();
                        ordemServico.IdTabBairro = repBairro.GetByDescricao(dcrBairro).IdTabBairro;
                        ordemServico.IdTabMunicipio = repMunicipio.GetByDescricao(dcrMunicipio).IdTabMunicipio;
                        ordemServico.IdTabServico = repServico.GetByDescricao(dcrServico).IdTabServico;
                        repOrdemServico.Add(ordemServico);
                    }

                    



                    //sMatricula = item["Matricula"].ToString();
                    //dDataAtendimento = DateTime.Parse(item["DataAtendimento"].ToString());
                    //iMatricula = int.Parse(sMatricula);
                    //var ordemServico = ctx.OrdensServicos.Where(x => x.MesRefLote == sMesRef && x.Matricula == iMatricula && x.Status != "").FirstOrDefault();
                    //if (ordemServico == null) continue;

                    //var roteiroDetalhe = ctx.RoteiroDetalhes.Where(x => x.IdOrdemServico == ordemServico.ID && x.Status != null && x.DataIniAtendimento != null).FirstOrDefault();
                    //if (roteiroDetalhe == null || roteiroDetalhe.IdOcorrencia1 == null) continue;

                    //ordemServico.Status = "ATENDIDO";
                    //roteiroDetalhe.Status = "ATENDIDO";
                    //roteiroDetalhe.DataIniAtendimento = dDataAtendimento;
                    //roteiroDetalhe.TipoPadrao = item["TipoPadrao"].ToString();
                    //roteiroDetalhe.LocalHD = item["LocalHD"].ToString();
                    //ctx.SaveChanges();
                }
            }
        }
    }
}