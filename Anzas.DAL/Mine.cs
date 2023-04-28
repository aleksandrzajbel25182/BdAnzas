using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Справочник
/// </summary>
public partial class Mine
{
    public int Uid { get; set; }

    /// <summary>
    /// Тип выработки
    /// </summary>
    public string TypeLcode { get; set; } = null!;

    public virtual ICollection<InfoDrill> InfoDrills { get; set; } = new List<InfoDrill>();

    public virtual ICollection<InfoTrench> InfoTrenches { get; set; } = new List<InfoTrench>();
}
