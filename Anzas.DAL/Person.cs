using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Справочник персонала
/// </summary>
public partial class Person
{
    public int Uid { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; set; } = null!;

    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; set; }

    public virtual ICollection<InfoDrill> InfoDrills { get; set; } = new List<InfoDrill>();

    public virtual ICollection<InfoTrench> InfoTrenches { get; set; } = new List<InfoTrench>();

    public virtual ICollection<Rock> Rocks { get; set; } = new List<Rock>();
}
