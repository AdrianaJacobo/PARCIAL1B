using System;
using System.Collections.Generic;

namespace PARCIAL1B.Models;

public partial class PlatoPorCombo
{
    public int PlatosPorComboId { get; set; }

    public int? EmpresaId { get; set; }

    public string? ComboId { get; set; }

    public int? PlatoId { get; set; }

    public string? Estado { get; set; }

    public virtual Plato? Plato { get; set; }
}
