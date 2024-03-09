using System;
using System.Collections.Generic;

namespace PARCIAL1B.Models;

public partial class Elemento
{
    public int ElementoId { get; set; }

    public int? EmpresaId { get; set; }

    public string? Elemento1 { get; set; }

    public int? CantidadMinima { get; set; }

    public string? UnidadMedida { get; set; }

    public decimal? Costo { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<ElementosPorPlato> ElementosPorPlatos { get; set; } = new List<ElementosPorPlato>();
}
