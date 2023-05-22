using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Литология
/// </summary>
public partial class Rock
{
    public int Uid { get; set; }

    /// <summary>
    /// № выработки
    /// </summary>
    public int HoleId { get; set; }

    /// <summary>
    /// Номер ПЛ
    /// </summary>
    public int? Profile { get; set; }

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
    /// Выход керна, м
    /// </summary>
    public double? Kernm { get; set; }

    /// <summary>
    /// Выход керна, %
    /// </summary>
    public double? Kernpc { get; set; }

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

    public virtual InfoDrill Hole { get; set; } = null!;

    public virtual RockCode? RockCodeNavigation { get; set; }
}
