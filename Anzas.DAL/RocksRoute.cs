using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Литология маршрутов
/// </summary>
public partial class RocksRoute
{
    public int Uid { get; set; }

    /// <summary>
    /// № маршрута
    /// </summary>
    public int HoleId { get; set; }

    /// <summary>
    /// Номер точки наблюдения (т.н.)
    /// </summary>
    public int? Rtn { get; set; }

    public string? Rsample { get; set; }

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
    /// Характеристика места отбора обр., проб
    /// </summary>
    public int? TnOtbor { get; set; }

    /// <summary>
    /// Тип опробуемых отложений
    /// </summary>
    public int? TnType { get; set; }

    /// <summary>
    /// Код породы
    /// </summary>
    public int? RockCode { get; set; }

    /// <summary>
    /// Описание керна
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Геолог
    /// </summary>
    public int? Geolog { get; set; }

    /// <summary>
    /// Описание шлифов
    /// </summary>
    public string? Petro { get; set; }

    /// <summary>
    /// Описание аншлифов
    /// </summary>
    public string? Mineral { get; set; }

    /// <summary>
    /// Примечания
    /// </summary>
    public string? NotesCommentsText { get; set; }

    public virtual Person? GeologNavigation { get; set; }

    public virtual RockCode? RockCodeNavigation { get; set; }

    public virtual Otbor? TnOtborNavigation { get; set; }

    public virtual Type? TnTypeNavigation { get; set; }
}
