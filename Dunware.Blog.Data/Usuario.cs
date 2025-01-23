using System;
using System.Collections.Generic;

namespace Dunware.Blog.Data;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Fecharegistro { get; set; }

    public bool Estado { get; set; }

    public string? PasswordHash { get; set; }

    public virtual ICollection<Post> Post { get; set; } = new List<Post>();
}
