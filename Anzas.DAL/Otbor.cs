using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Справочник характеристика места отбора обр., проб
/// </summary>
public partial class Otbor
{
    public int Uid { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RocksRoute> RocksRoutes { get; set; } = new List<RocksRoute>();
}
