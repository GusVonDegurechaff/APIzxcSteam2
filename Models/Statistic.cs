using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Statistic
{
    public int StatId { get; set; }

    public int ProfileId { get; set; }

    public string HoursInGame { get; set; } = null!;

    public string Rank { get; set; } = null!;

    public string Lvl { get; set; } = null!;

    public virtual Profile Profile { get; set; } = null!;
}
