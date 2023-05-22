using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Информация по скважинам
/// </summary>
public partial class InfoDrill
{
    public int Uid { get; set; }

    /// <summary>
    /// № скважины
    /// </summary>
    public string? HoleId { get; set; }

    /// <summary>
    /// Тип выработки
    /// </summary>
    public int? TypeLcode { get; set; }

    /// <summary>
    /// Название участка
    /// </summary>
    public int? PlaceSite { get; set; }

    /// <summary>
    /// Номер ПЛ
    /// </summary>
    public double? Profile { get; set; }

    /// <summary>
    /// Долгота
    /// </summary>
    public double? Easting { get; set; }

    /// <summary>
    /// Широта
    /// </summary>
    public double? Northing { get; set; }

    /// <summary>
    /// Абс. отм.
    /// </summary>
    public double? Elevation { get; set; }

    /// <summary>
    /// Диаметр бурения, мм
    /// </summary>
    public double? Diam { get; set; }

    /// <summary>
    /// Азимут ист., °
    /// </summary>
    public double? Azimuth { get; set; }

    /// <summary>
    /// Угол наклона от горизонта, °
    /// </summary>
    public double? Dip { get; set; }

    /// <summary>
    /// Глубина скважины,м
    /// </summary>
    public double? Depth { get; set; }

    /// <summary>
    /// Уровень ПВ, м
    /// </summary>
    public double? Uroven { get; set; }

    /// <summary>
    /// Абс. отм. уровня, м
    /// 
    /// </summary>
    public double? UrAbs { get; set; }

    /// <summary>
    /// Начало бурения
    /// </summary>
    public DateOnly? StartDate { get; set; }

    /// <summary>
    /// Окончание бурения
    /// </summary>
    public DateOnly? EndDate { get; set; }

    /// <summary>
    /// Геолог
    /// </summary>
    public int? Geolog { get; set; }

    /// <summary>
    /// Примечания
    /// </summary>
    public string? NotesCommentsText { get; set; }

    public int Project { get; set; }

    public virtual Person? GeologNavigation { get; set; }

    public virtual Place? PlaceSiteNavigation { get; set; }

    public virtual Project ProjectNavigation { get; set; } = null!;

    public virtual ICollection<Rock> Rocks { get; set; } = new List<Rock>();

    public virtual Mine? TypeLcodeNavigation { get; set; }
}
