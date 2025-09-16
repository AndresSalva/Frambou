using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

[Table("Vehiculo")]
public partial class Vehiculo
{
    #region Properties
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("marca")]
    [StringLength(20)]
    [Unicode(false)]
    public string Marca { get; set; } = null!;

    [Column("modelo")]
    [StringLength(20)]
    [Unicode(false)]
    public string Modelo { get; set; } = null!;

    [Column("placa")]
    [StringLength(10)]
    [Unicode(false)]
    public string Placa { get; set; } = null!;

    [Column("kilometraje")]
    public int Kilometraje { get; set; }

    [Column("capacidadMotor")]
    public int CapacidadMotor { get; set; }

    /// <summary>
    /// Gasolina = 0,
    /// Diesel = 1,
    /// Hibrido = 2,
    /// Electrico = 3
    /// </summary>
    [Column("combustible")]
    public byte Combustible { get; set; }

    /// <summary>
    /// Manual = 0, 
    /// Automática = 1, 
    /// CVT = 2
    /// </summary>
    [Column("transmision")]
    public byte Transmision { get; set; }

    [Column("idCliente")]
    public int IdCliente { get; set; }

    [Column("fechaRegistro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("Vehiculos")]
    public virtual Cliente? IdClienteNavigation { get; set; } = null!;

    [InverseProperty("IdVehiculoNavigation")]
    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>(); 
    #endregion
}
