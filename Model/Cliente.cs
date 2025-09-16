using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

[Table("Cliente")]
public partial class Cliente
{
    #region Properties
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Cliente")]
    public virtual Usuario IdNavigation { get; set; } = null!;

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>(); 
    #endregion
}
