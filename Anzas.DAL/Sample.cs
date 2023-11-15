using System;
using System.Collections.Generic;

namespace Anzas.DAL;

public partial class Sample
{
    public int Uid { get; set; }

    /// <summary>
    /// № выработки
    /// </summary>
    public int? HoleId { get; set; }

    /// <summary>
    /// От
    /// </summary>
    public double? From { get; set; }

    /// <summary>
    /// До
    /// </summary>
    public double? To { get; set; }

    /// <summary>
    /// Длина интервала
    /// </summary>
    public double? Length { get; set; }

    /// <summary>
    /// Геологический номер
    /// </summary>
    public string? GeoSample { get; set; }

    /// <summary>
    /// Код породы
    /// </summary>
    public int? RockCode { get; set; }

    /// <summary>
    /// Вес пробы
    /// </summary>
    public double? Weight { get; set; }

    /// <summary>
    /// Тип пробы
    /// </summary>
    public int? SampleType { get; set; }

    /// <summary>
    /// Геолог
    /// </summary>
    public int? Geolog { get; set; }

    /// <summary>
    /// Комментарии
    /// </summary>
    public string? NotesCommentsText { get; set; }

    /// <summary>
    /// Тип выработки (скважина/траншея/маршрут)
    /// </summary>
    public int TypeHole { get; set; }

    public int? Profile { get; set; }

    public virtual TypeHole TypeHoleNavigation { get; set; } = null!;
}
