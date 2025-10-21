using System;
using System.Collections.Generic;

namespace Web.DataBase;

public partial class TabUsuarios
{
    public int IdTabUsuarios { get; set; }

    public string Matricula { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public string Senha { get; set; } = null!;
}
