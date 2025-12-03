using System;
using System.Collections.Generic;

namespace Web.DataBase;

public partial class OrdensServicosRej
{
    public int IdOrdemServicoRej { get; set; }

    public int IdOrdemServico { get; set; }

    public string? Motivo { get; set; }

    public virtual OrdensServicos IdOrdemServicoNavigation { get; set; } = null!;
}
