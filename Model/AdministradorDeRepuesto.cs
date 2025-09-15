using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

public partial class AdministradorDeRepuesto
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("AdministradorDeRepuesto")]
    public virtual Empleado IdNavigation { get; set; } = null!;

    [InverseProperty("IdAdminRepuestosNavigation")]
    public virtual ICollection<InventarioDeRepuesto> InventarioDeRepuestos { get; set; } = new List<InventarioDeRepuesto>();
}
