using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

public partial class InventarioDeRepuesto
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idAdminRepuestos")]
    public int IdAdminRepuestos { get; set; }

    [Column("fechaRegistro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("ultimaActualizacion", TypeName = "datetime")]
    public DateTime UltimaActualizacion { get; set; }

    [ForeignKey("IdAdminRepuestos")]
    [InverseProperty("InventarioDeRepuestos")]
    public virtual AdministradorDeRepuesto IdAdminRepuestosNavigation { get; set; } = null!;

    [InverseProperty("IdInventarioNavigation")]
    public virtual ICollection<Repuesto> Repuestos { get; set; } = new List<Repuesto>();
}
