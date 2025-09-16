using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

[Table("Empleado")]
public partial class Empleado
{
    #region Properties
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("direccion")]
    [StringLength(100)]
    [Unicode(false)]
    public string Direccion { get; set; } = null!;

    [Column("salarioBasico", TypeName = "decimal(8, 2)")]
    public decimal SalarioBasico { get; set; }

    [Column("cargo")]
    [StringLength(50)]
    [Unicode(false)]
    public string Cargo { get; set; } = null!;

    [InverseProperty("IdNavigation")]
    public virtual AdministradorDeProgramacion? AdministradorDeProgramacion { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual AdministradorDeRepuesto? AdministradorDeRepuesto { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Empleado")]
    public virtual Usuario? IdNavigation { get; set; } = null!; 
    #endregion
}
