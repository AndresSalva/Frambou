using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HospitalDeVehiculosUltimaVersion.Model;

public partial class HospitalDeVehiculosContext : DbContext
{
    public HospitalDeVehiculosContext()
    {
    }

    public HospitalDeVehiculosContext(DbContextOptions<HospitalDeVehiculosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdministradorDeProgramacion> AdministradorDeProgramacions { get; set; }

    public virtual DbSet<AdministradorDeRepuesto> AdministradorDeRepuestos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<InventarioDeRepuesto> InventarioDeRepuestos { get; set; }

    public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

    public virtual DbSet<MantenimientoPago> MantenimientoPagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Repuesto> Repuestos { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdministradorDeProgramacion>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.AdministradorDeProgramacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdministradorDeProgramacion_Empleado");
        });

        modelBuilder.Entity<AdministradorDeRepuesto>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.AdministradorDeRepuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdministradorDeRepuestos_Empleado");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Cliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Usuario");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Empleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_Usuario");
        });

        modelBuilder.Entity<InventarioDeRepuesto>(entity =>
        {
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdAdminRepuestosNavigation).WithMany(p => p.InventarioDeRepuestos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventarioDeRepuestos_AdministradorDeRepuestos");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.Property(e => e.Estado)
                .HasDefaultValue((byte)1)
                .HasComment("Cancelado =0,\r\nProgramado = 1\r\nEn Progreso =2,\r\nCompletado = 3,");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdAdminMantenimientoNavigation).WithMany(p => p.Mantenimientos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mantenimiento_AdministradorDeProgramacion");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.Mantenimientos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mantenimiento_Vehiculo");
        });

        modelBuilder.Entity<MantenimientoPago>(entity =>
        {
            entity.HasOne(d => d.IdMantenimientoNavigation).WithMany().HasConstraintName("FK_MantenimientoPago_Mantenimiento");

            entity.HasOne(d => d.IdPagoNavigation).WithMany().HasConstraintName("FK_MantenimientoPago_Pago");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.Property(e => e.Divisa).IsFixedLength();
            entity.Property(e => e.Estado).HasComment("0 = Pendiente,\r\n1 = Pagado,\r\n2 = Anulado,\r\n3 = Reembolsado");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pagos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Cliente");
        });

        modelBuilder.Entity<Repuesto>(entity =>
        {
            entity.Property(e => e.EstadoDeUso).HasComment("Nuevo = 1,\r\nUsado = 0,\r\n");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.Repuestos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Repuestos_InventarioDeRepuestos");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.Property(e => e.Estado)
                .HasDefaultValue((byte)1)
                .HasComment("Cancelado = 0,\r\nEn Espera = 1,\r\nEn progreso = 2,\r\nFinalizado = 3");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdMantenimientoNavigation).WithMany(p => p.Servicios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Servicio_Mantenimiento");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Estado)
                .HasDefaultValue((byte)1)
                .HasComment("Inactivo = 0,\r\nActivo = 1,");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.Property(e => e.Combustible).HasComment("Gasolina = 0,\r\nDiesel = 1,\r\nHibrido = 2,\r\nElectrico = 3");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Transmision).HasComment("Manual = 0, \r\nAutomática = 1, \r\nCVT = 2");
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Vehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehiculo_Cliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
