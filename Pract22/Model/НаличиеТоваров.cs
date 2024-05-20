using System;
using System.Collections.Generic;

namespace Pract22.Model;

public partial class НаличиеТоваров
{
    public int НомерСклада { get; set; }

    public int КодТовара { get; set; }

    public int Количество { get; set; }

    public virtual Товары КодТовараNavigation { get; set; } = null!;

    public virtual Склады НомерСкладаNavigation { get; set; } = null!;
}
