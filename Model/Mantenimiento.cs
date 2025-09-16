using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

[Table("Mantenimiento")]
public partial class Mantenimiento
{
    #region Properties
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idVehiculo")]
    public int IdVehiculo { get; set; }

    [Column("fechaProgramada", TypeName = "datetime")]
    public DateTime FechaProgramada { get; set; }

    [Column("fechaEjecucion", TypeName = "datetime")]
    public DateTime? FechaEjecucion { get; set; }

    /// <summary>
    /// Cancelado =0,
    /// Programado = 1
    /// En Progreso =2,
    /// Completado = 3,
    /// </summary>
    [Column("estado")]
    public byte Estado { get; set; }

    [Column("fechaRegistro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [Column("idAdminMantenimiento")]
    public int IdAdminMantenimiento { get; set; }

    [ForeignKey("IdAdminMantenimiento")]
    [InverseProperty("Mantenimientos")]
    public virtual AdministradorDeProgramacion IdAdminMantenimientoNavigation { get; set; } = null!;

    [ForeignKey("IdVehiculo")]
    [InverseProperty("Mantenimientos")]
    public virtual Vehiculo? IdVehiculoNavigation { get; set; } = null!;

    [InverseProperty("IdMantenimientoNavigation")]
    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>(); 
    #endregion
}
