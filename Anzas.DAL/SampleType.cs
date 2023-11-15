using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Справочник типов опробования
/// </summary>
public partial class SampleType
{
    public int Uid { get; set; }

    public string? TypeLcode { get; set; }
}
