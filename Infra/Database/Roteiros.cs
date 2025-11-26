using System;
using System.Collections.Generic;

namespace Infra.DataBase;

public partial class Roteiros
{
    public int IdRoteiro { get; set; }

    public DateOnly? Data { get; set; }

    public int? IdTabAgente { get; set; }

    public DateTime? DataUltComunicacao { get; set; }

    public virtual TabAgentes? IdTabAgenteNavigation { get; set; }

    public virtual ICollection<RoteiroDetalhes> RoteiroDetalhes { get; set; } = new List<RoteiroDetalhes>();
}
