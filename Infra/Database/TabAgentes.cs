using System;
using System.Collections.Generic;

namespace Infra.DataBase;

public partial class TabAgentes
{
    public int IdTabAgente { get; set; }

    public string? Nome { get; set; }

    public string? Status { get; set; }

    public string? Matricula { get; set; }

    public string? IdContrato { get; set; }

    public string? Cargo { get; set; }

    public virtual ICollection<Roteiros> Roteiros { get; set; } = new List<Roteiros>();
}
