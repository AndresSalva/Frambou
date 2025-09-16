using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

[Keyless]
[Table("MantenimientoPago")]
public partial class MantenimientoPago
{
    #region Properties
    [Column("idMantenimiento")]
    public int? IdMantenimiento { get; set; }

    [Column("idPago")]
    public int? IdPago { get; set; }

    [ForeignKey("IdMantenimiento")]
    public virtual Mantenimiento? IdMantenimientoNavigation { get; set; }

    [ForeignKey("IdPago")]
    public virtual Pago? IdPagoNavigation { get; set; } 
    #endregion
}
