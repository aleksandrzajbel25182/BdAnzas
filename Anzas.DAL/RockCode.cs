using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Справочник породы
/// </summary>
public partial class RockCode
{
    public int Uid { get; set; }

    /// <summary>
    /// Код породы
    /// </summary>
    public string RockCode1 { get; set; } = null!;

    /// <summary>
    /// Порода
    /// </summary>
    public string? Rock { get; set; }

    public virtual ICollection<Rock> Rocks { get; set; } = new List<Rock>();

    public virtual ICollection<RocksRoute> RocksRoutes { get; set; } = new List<RocksRoute>();
}
