using System;
using System.Collections.Generic;

namespace BusinessLogic.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Dob { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
