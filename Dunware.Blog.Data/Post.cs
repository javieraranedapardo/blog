using System;
using System.Collections.Generic;

namespace Dunware.Blog.Data;

public partial class Post
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public int Usuarioid { get; set; }

    public int? Categoriaid { get; set; }

    public DateTime Fechacreacion { get; set; }

    public DateTime Ultimaactualizacion { get; set; }

    public bool Estado { get; set; }

    public string Slug { get; set; } = null!;

    public virtual Categoria? Categoria { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
