using System;
using System.Collections.Generic;

namespace Dunware.Blog.Data;

public partial class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime Fechacreacion { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Post> Post { get; set; } = new List<Post>();
}
