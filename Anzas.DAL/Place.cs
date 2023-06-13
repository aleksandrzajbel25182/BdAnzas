using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Справочник Участков
/// </summary>
public partial class Place
{
    public int Uid { get; set; }

    /// <summary>
    /// Название участка
    /// </summary>
    public string NamePlaceSite { get; set; } = null!;

    public virtual ICollection<InfoDrill> InfoDrills { get; set; } = new List<InfoDrill>();

    public virtual ICollection<InfoRoute> InfoRoutes { get; set; } = new List<InfoRoute>();

    public virtual ICollection<InfoTrench> InfoTrenches { get; set; } = new List<InfoTrench>();
}
