using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Game
{
    public int GameId { get; set; }

    public string GameName { get; set; } = null!;

    public string Developer { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public string? Description { get; set; }

    public string? Genre { get; set; }

    public string? CoverImage { get; set; }

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public virtual ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();
}
