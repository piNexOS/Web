using System;
using System.Collections.Generic;

namespace Web.DataBase;

public partial class TabAgentes
{
    public int IdTabAgente { get; set; }

    public string? Nome { get; set; }

    public string? Status { get; set; }

    public string? Matricula { get; set; }

    public virtual ICollection<Roteiros> Roteiros { get; set; } = new List<Roteiros>();
}
