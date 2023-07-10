using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Информация по канавам/траншеям
/// </summary>
public partial class InfoTrench
{
    public int Uid { get; set; }

    /// <summary>
    /// № выработки
    /// </summary>
    public string HoleId { get; set; } = null!;

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
    /// Долгота (начало)
    /// </summary>
    public double? Easting1 { get; set; }

    /// <summary>
    /// Широта (начало)
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
    /// Азимут ист., °
    /// </summary>
    public double? Azimuth { get; set; }

    /// <summary>
    /// Длина канавы,м
    /// </summary>
    public double? Length { get; set; }

    /// <summary>
    /// Глубина канавы,м
    /// </summary>
    public double? Depth { get; set; }

    /// <summary>
    /// Ширина канавы,м
    /// </summary>
    public double? Width { get; set; }

    /// <summary>
    /// Начало проходки
    /// </summary>
    public DateOnly? StartDate { get; set; }

    /// <summary>
    /// Окончание проходки
    /// </summary>
    public DateOnly? EndDate { get; set; }

    /// <summary>
    /// Геолог
    /// </summary>
    public int Geolog { get; set; }

    /// <summary>
    /// Примечания
    /// </summary>
    public string? NotesCommentsText { get; set; }

    public virtual Person GeologNavigation { get; set; } = null!;

    public virtual Place? PlaceSiteNavigation { get; set; }

    public virtual ICollection<SurveyTrench> SurveyTrenches { get; set; } = new List<SurveyTrench>();

    public virtual Mine? TypeLcodeNavigation { get; set; }
}
