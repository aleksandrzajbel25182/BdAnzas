using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Справочник тип опробуемых отложений
/// </summary>
public partial class Type
{
    public int Uid { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = null!;

    public virtual ICollection<RocksRoute> RocksRoutes { get; set; } = new List<RocksRoute>();
}
