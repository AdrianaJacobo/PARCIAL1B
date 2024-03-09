using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PARCIAL1B.Models;

public partial class Parcial1bContext : DbContext
{
    public Parcial1bContext()
    {
    }

    public Parcial1bContext(DbContextOptions<Parcial1bContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Elemento> Elementos { get; set; }

    public virtual DbSet<ElementosPorPlato> ElementosPorPlatos { get; set; }

    public virtual DbSet<Plato> Platos { get; set; }

    public virtual DbSet<PlatoPorCombo> PlatoPorCombos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KCKNA65; Database=PARCIAL1B; Trusted_Connection=True; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Elemento>(entity =>
        {
            entity.HasKey(e => e.ElementoId).HasName("PK_elementos");

            entity.Property(e => e.ElementoId).HasColumnName("ElementoID");
            entity.Property(e => e.Costo)
                .HasColumnType("numeric(18, 4)")
                .HasColumnName("costo");
            entity.Property(e => e.Elemento1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Elemento");
            entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ElementosPorPlato>(entity =>
        {
            entity.HasKey(e => e.ElementoPorPlatoId).HasName("PK_elementosporplato");

            entity.ToTable("ElementosPorPlato");

            entity.Property(e => e.ElementoPorPlatoId).HasColumnName("ElementoPorPlatoID");
            entity.Property(e => e.ElementoId).HasColumnName("ElementoID");
            entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlatoId).HasColumnName("PlatoID");

            entity.HasOne(d => d.Elemento).WithMany(p => p.ElementosPorPlatos)
                .HasForeignKey(d => d.ElementoId)
                .HasConstraintName("FK_ElementosPorPlato_Elementos");

            entity.HasOne(d => d.Plato).WithMany(p => p.ElementosPorPlatos)
                .HasForeignKey(d => d.PlatoId)
                .HasConstraintName("FK_ElementosPorPlato_Platos");
        });

        modelBuilder.Entity<Plato>(entity =>
        {
            entity.HasKey(e => e.PlatoId).HasName("PK_platos");

            entity.Property(e => e.PlatoId).HasColumnName("PlatoID");
            entity.Property(e => e.Costo)
                .HasColumnType("numeric(18, 4)")
                .HasColumnName("costo");
            entity.Property(e => e.DescripcionPlato)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");
            entity.Property(e => e.GrupoId).HasColumnName("GrupoID");
            entity.Property(e => e.NombrePlato)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PlatoPorCombo>(entity =>
        {
            entity.HasKey(e => e.PlatosPorComboId);

            entity.ToTable("PlatoPorCombo");

            entity.Property(e => e.PlatosPorComboId).HasColumnName("PlatosPorComboID");
            entity.Property(e => e.ComboId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ComboID");
            entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlatoId).HasColumnName("PlatoID");

            entity.HasOne(d => d.Plato).WithMany(p => p.PlatoPorCombos)
                .HasForeignKey(d => d.PlatoId)
                .HasConstraintName("FK_PlatoPorCombo_Platos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
