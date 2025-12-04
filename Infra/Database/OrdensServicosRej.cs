using System;
using System.Collections.Generic;

namespace Infra.DataBase;

public partial class OrdensServicosRej
{
    public int IdOrdemServicoRej { get; set; }

    public int IdRoteiroDetalhes { get; set; }

    public string? Motivo { get; set; }

    public virtual RoteiroDetalhes IdRoteiroDetalhesNavigation { get; set; } = null!;
}
