using System;
using System.Collections.Generic;

namespace Web.DataBase;

public partial class RoteiroDetalhes
{
    public int IdRoteiroDetalhe { get; set; }

    public int? IdRoteiro { get; set; }

    public int? IdOrdemServico { get; set; }

    public DateTime? DataIniAtendimento { get; set; }

    public DateTime? DataFimAtendimento { get; set; }

    public string? Status { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public DateTime? HoraDownload { get; set; }

    public DateTime? HoraUpload { get; set; }

    public virtual OrdensServicos? IdOrdemServicoNavigation { get; set; }

    public virtual Roteiros? IdRoteiroNavigation { get; set; }
}
