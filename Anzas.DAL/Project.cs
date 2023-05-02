using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Проекты
/// </summary>
public partial class Project
{
    public int Uid { get; set; }

    public string Name { get; set; } = null!;

    public string? NameFull { get; set; }

    public int? OrgExecutor { get; set; }

    public virtual ICollection<InfoDrill> InfoDrills { get; set; } = new List<InfoDrill>();
}
