using System;
using System.Collections.Generic;

namespace Infra.DataBase;

public partial class OrdensServicos
{
    public int IdOrdemServico { get; set; }

    public string? NumeroOS { get; set; }

    public string? Matricula { get; set; }

    public int? IdTabServico { get; set; }

    public int? IdTabBairro { get; set; }

    public int? IdTabMunicipio { get; set; }

    public string? Informacoes { get; set; }

    public string? Referencia { get; set; }

    public string? Endereco { get; set; }

    public string? NumeroCasa { get; set; }

    public string? Complemento { get; set; }

    public string? NomeSolicitante { get; set; }

    public string? TelSolicitante { get; set; }

    public string? NomeCliente { get; set; }

    public string? CPF { get; set; }

    public string? CNPJ { get; set; }

    public string? TelCliente { get; set; }

    public string? NumHD { get; set; }

    public string? UltimaLeitura { get; set; }

    public string? TipoPadrao { get; set; }

    public string? LocalizacaoPadrao { get; set; }

    public string? Ciclo { get; set; }

    public string? Classificacao { get; set; }

    public DateTime? DataRegistro { get; set; }

    public DateTime? DataRecepcao { get; set; }

    public DateTime? DataLimite { get; set; }

    public DateTime? DataImportacao { get; set; }

    public string? Status { get; set; }

    public DateTime? DataBaixa { get; set; }

    public virtual TabBairros? IdTabBairroNavigation { get; set; }

    public virtual TabMunicipios? IdTabMunicipioNavigation { get; set; }

    public virtual TabServicos? IdTabServicoNavigation { get; set; }

    public virtual ICollection<OrdensServicosRej> OrdensServicosRej { get; set; } = new List<OrdensServicosRej>();

    public virtual ICollection<RoteiroDetalhes> RoteiroDetalhes { get; set; } = new List<RoteiroDetalhes>();
}
