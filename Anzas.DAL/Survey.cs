using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Инклинометрия скважин
/// </summary>
public partial class Survey
{
    public int Uid { get; set; }

    public int HoleId { get; set; }

    /// <summary>
    /// Глубина
    /// </summary>
    public double? Depth { get; set; }

    /// <summary>
    /// Угол наклона от вертикали
    /// </summary>
    public double? Inclin { get; set; }

    /// <summary>
    /// Аз_магн_исходный
    /// </summary>
    public double? AzMagn { get; set; }

    /// <summary>
    /// Аз_мг_принятый
    /// </summary>
    public double? AzimPr { get; set; }

    /// <summary>
    /// Аз_ист_принятый
    /// </summary>
    public double? AzimIst { get; set; }

    public virtual InfoDrill Hole { get; set; } = null!;
}
