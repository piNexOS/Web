using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.DataBase;

public partial class TabUsuarios
{
    public int IdTabUsuarios { get; set; }

    public string Matricula { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    [DataType(DataType.Password)]
    public string Senha { get; set; } = null!;
}
