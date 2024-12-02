using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? RegistrationDate { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? ProfilePicture { get; set; }

    public virtual ICollection<Friend> Friends { get; set; } = new List<Friend>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public virtual ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();
}
