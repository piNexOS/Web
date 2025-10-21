using System;
using System.Collections.Generic;

namespace Infra.DataBase;

public partial class TabClientes
{
    public int IdTableCliente { get; set; }

    public string? CPF { get; set; }

    public string? CNPJ { get; set; }

    public string? Nome { get; set; }

    public string? Telefone { get; set; }
}
