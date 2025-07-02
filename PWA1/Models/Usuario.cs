using System;
using System.Collections.Generic;

namespace PWA1.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public string? Pais { get; set; }

    public string? Sexo { get; set; }

    public virtual ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();
}


