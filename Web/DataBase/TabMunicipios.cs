using System;
using System.Collections.Generic;

namespace Web.DataBase;

public partial class TabMunicipios
{
    public int IdTabMunicipio { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<OrdensServicos> OrdensServicos { get; set; } = new List<OrdensServicos>();

    public virtual ICollection<TabBairros> TabBairros { get; set; } = new List<TabBairros>();
}
