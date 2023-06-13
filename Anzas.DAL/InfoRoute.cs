using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Информация по маршрутам
/// </summary>
public partial class InfoRoute
{
    public int Uid { get; set; }

    /// <summary>
    /// № маршрута
    /// </summary>
    public string HoleId { get; set; } = null!;

    /// <summary>
    /// Тип выработки
    /// </summary>
    public int TypeLcode { get; set; }

    /// <summary>
    /// Название участка
    /// </summary>
    public int PlaceSite { get; set; }

    /// <summary>
    /// Долгота (начало)
    /// </summary>
    public double? Easting1 { get; set; }

    /// <summary>
    /// Долгота (начало)
    /// </summary>
    public double? Northing1 { get; set; }

    /// <summary>
    /// Абс. отм. (начало)
    /// </summary>
    public double? Elevation1 { get; set; }

    /// <summary>
    /// Долгота (конец)
    /// </summary>
    public double? Easting2 { get; set; }

    /// <summary>
    /// Широта (конец)
    /// </summary>
    public double? Northing2 { get; set; }

    /// <summary>
    /// Абс. отм. (конец)
    /// </summary>
    public double? Elevation2 { get; set; }

    /// <summary>
    /// Длина маршрута
    /// </summary>
    public double? Length { get; set; }

    /// <summary>
    /// Дата
    /// </summary>
    public DateOnly? Date { get; set; }

    public int Geolog { get; set; }

    public string? NotesCommentsText { get; set; }

    public virtual Person GeologNavigation { get; set; } = null!;

    public virtual Place PlaceSiteNavigation { get; set; } = null!;

    public virtual Mine TypeLcodeNavigation { get; set; } = null!;
}
