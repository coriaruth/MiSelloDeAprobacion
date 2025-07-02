using System;
using System.Collections.Generic;

namespace PWA1.Models;

public partial class Subcategorium
{
    public int SubcategoriaId { get; set; }

    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual Categorium Categoria { get; set; } = null!;

    public virtual ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();
}
