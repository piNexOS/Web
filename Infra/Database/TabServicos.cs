using System;
using System.Collections.Generic;

namespace Infra.DataBase;

public partial class TabServicos
{
    public int IdTabServico { get; set; }

    public string? Descricao { get; set; }

    public string? CodServico { get; set; }

    public virtual ICollection<OrdensServicos> OrdensServicos { get; set; } = new List<OrdensServicos>();
}
