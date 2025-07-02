using System;
using System.Collections.Generic;

namespace PWA1.Models;

public partial class Reseña
{
    public int ReseñaId { get; set; }

    public int UsuarioId { get; set; }

    public int CategoriaId { get; set; }

    public int? SubcategoriaId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public int? PuntuacionCalidad { get; set; }

    public int? PuntuacionPrecio { get; set; }

    public int? PuntuacionDurabilidad { get; set; }

    public DateTime? FechaReseña { get; set; }

    public string? ImagenRuta { get; set; }

    public virtual Categorium Categoria { get; set; } = null!;

    public virtual Subcategorium? Subcategoria { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
