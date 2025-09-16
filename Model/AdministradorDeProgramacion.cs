using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

[Table("AdministradorDeProgramacion")]
public partial class AdministradorDeProgramacion
{
    #region Properties
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("AdministradorDeProgramacion")]
    public virtual Empleado IdNavigation { get; set; } = null!;

    [InverseProperty("IdAdminMantenimientoNavigation")]
    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>(); 
    #endregion
}
