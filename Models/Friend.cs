﻿using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Friend
{
    public int FriendshipId { get; set; }

    public int UserId1 { get; set; }

    public int UserId2 { get; set; }

    public virtual User UserId1Navigation { get; set; } = null!;
}
