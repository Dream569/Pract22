using System;
using System.Collections.Generic;

namespace Pract22.Model;

public partial class Склады
{
    public int Номер { get; set; }

    public string АдресСклада { get; set; } = null!;

    public int Телефон { get; set; }

    public string Фиоруководителя { get; set; } = null!;

    public string? Photo { get; set; }

    public virtual ICollection<НаличиеТоваров> НаличиеТоваровs { get; set; } = new List<НаличиеТоваров>();
}
