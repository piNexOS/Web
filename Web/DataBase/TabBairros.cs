using System;
using System.Collections.Generic;

namespace Web.DataBase;

public partial class TabBairros
{
    public int IdTabBairro { get; set; }

    public string Descricao { get; set; } = null!;

    public int IdTabMunicipio { get; set; }

    public virtual TabMunicipios IdTabMunicipioNavigation { get; set; } = null!;

    public virtual ICollection<OrdensServicos> OrdensServicos { get; set; } = new List<OrdensServicos>();
}
