using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

public partial class Repuesto
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("precio", TypeName = "decimal(8, 2)")]
    public decimal Precio { get; set; }

    [Column("stockActual")]
    public int StockActual { get; set; }

    [Column("stockMinimo")]
    public int StockMinimo { get; set; }

    [Column("idInventario")]
    public int IdInventario { get; set; }

    /// <summary>
    /// Nuevo = 1,
    /// Usado = 0,
    /// 
    /// </summary>
    [Column("estadoDeUso")]
    public byte EstadoDeUso { get; set; }

    [Column("fechaRegistro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [ForeignKey("IdInventario")]
    [InverseProperty("Repuestos")]
    public virtual InventarioDeRepuesto IdInventarioNavigation { get; set; } = null!;
}
