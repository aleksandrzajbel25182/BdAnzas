using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Инклинометрия траншеи
/// </summary>
public partial class SurveyTrench
{
    public int Uid { get; set; }

    public int? SurveyTrench1 { get; set; }

    /// <summary>
    /// Номер точки поворота
    /// </summary>
    public int? NumTp { get; set; }

    /// <summary>
    /// Длина канавы,м
    /// </summary>
    public double? Length { get; set; }

    /// <summary>
    /// Азимут магн. полевой, °
    /// </summary>
    public double? AzMagn { get; set; }

    /// <summary>
    /// Глубина канавы,м
    /// </summary>
    public double? Depth { get; set; }

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
}
