using System;
using System.Collections.Generic;

namespace Anzas.DAL;

/// <summary>
/// Тип выработк
/// </summary>
public partial class TypeHole
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Sample> Samples { get; set; } = new List<Sample>();
}
