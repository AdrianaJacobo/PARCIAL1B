using System;
using System.Collections.Generic;

namespace PARCIAL1B.Models;

public partial class Plato
{
    public int PlatoId { get; set; }

    public int? EmpresaId { get; set; }

    public int? GrupoId { get; set; }

    public string? NombrePlato { get; set; }

    public string? DescripcionPlato { get; set; }

    public decimal? Costo { get; set; }

    public virtual ICollection<ElementosPorPlato> ElementosPorPlatos { get; set; } = new List<ElementosPorPlato>();

    public virtual ICollection<PlatoPorCombo> PlatoPorCombos { get; set; } = new List<PlatoPorCombo>();
}
