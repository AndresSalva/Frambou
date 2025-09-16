using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

[Table("Pago")]
public partial class Pago
{
    #region Properties
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idCliente")]
    public int IdCliente { get; set; }

    [Column("fechaRegistro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("divisa")]
    [StringLength(3)]
    public string Divisa { get; set; } = null!;

    [Column("subtotal", TypeName = "decimal(8, 2)")]
    public decimal Subtotal { get; set; }

    [Column("descuento", TypeName = "decimal(8, 2)")]
    public decimal Descuento { get; set; }

    [Column("total", TypeName = "decimal(8, 2)")]
    public decimal Total { get; set; }

    /// <summary>
    /// 0 = Pendiente,
    /// 1 = Pagado,
    /// 2 = Anulado,
    /// 3 = Reembolsado
    /// </summary>
    [Column("estado")]
    public byte Estado { get; set; }


    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("Pagos")]
    public virtual Cliente? IdClienteNavigation { get; set; } = null!; 
    #endregion
}
