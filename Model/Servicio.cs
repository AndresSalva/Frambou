using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

[Table("Servicio")]
public partial class Servicio
{
    #region Properties
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(30)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [Column("precio", TypeName = "decimal(8, 2)")]
    public decimal Precio { get; set; }

    [Column("idMantenimiento")]
    public int IdMantenimiento { get; set; }

    /// <summary>
    /// Cancelado = 0,
    /// En Espera = 1,
    /// En progreso = 2,
    /// Finalizado = 3
    /// </summary>
    [Column("estado")]
    public byte Estado { get; set; }

    [Column("fechaRegistro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("lastUpdate", TypeName = "datetime")]
    public DateTime LastUpdate { get; set; }

    [ForeignKey("IdMantenimiento")]
    [InverseProperty("Servicios")]
    public virtual Mantenimiento? IdMantenimientoNavigation { get; set; } = null!; 
    #endregion
}
