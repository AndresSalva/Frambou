using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

[Table("Usuario")]
public partial class Usuario
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("primerNombre")]
    [StringLength(90)]
    [Unicode(false)]
    public string PrimerNombre { get; set; } = null!;

    [Column("segundoNombre")]
    [StringLength(90)]
    [Unicode(false)]
    public string? SegundoNombre { get; set; }

    [Column("primerApellido")]
    [StringLength(90)]
    [Unicode(false)]
    public string PrimerApellido { get; set; } = null!;

    [Column("segundoApellido")]
    [StringLength(90)]
    [Unicode(false)]
    public string? SegundoApellido { get; set; }

    [Column("ci")]
    [StringLength(20)]
    [Unicode(false)]
    public string Ci { get; set; } = null!;

    [Column("numeroContacto")]
    [StringLength(12)]
    [Unicode(false)]
    public string NumeroContacto { get; set; } = null!;

    [Column("email")]
    [StringLength(20)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("contrasenia")]
    [StringLength(30)]
    [Unicode(false)]
    public string Contrasenia { get; set; } = null!;

    /// <summary>
    /// Inactivo = 0,
    /// Activo = 1,
    /// </summary>
    [Column("estado")]
    public byte Estado { get; set; }

    [Column("fechaRegistro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Cliente? Cliente { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Empleado? Empleado { get; set; }
}
