using System;
using System.Collections.Generic;

namespace Pract22.Model;

public partial class Товары
{
    public int Код { get; set; }

    public string Название { get; set; } = null!;

    public string ГруппаТоваров { get; set; } = null!;

    public string ФирмаПроизводитель { get; set; } = null!;

    public virtual ICollection<НаличиеТоваров> НаличиеТоваровs { get; set; } = new List<НаличиеТоваров>();
}
