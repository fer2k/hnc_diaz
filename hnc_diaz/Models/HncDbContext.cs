using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace hnc_diaz.Models;

public partial class HncDbContext : DbContext
{
    public HncDbContext()
    {
    }

    public HncDbContext(DbContextOptions<HncDbContext> options)
        : base(options)
    {
    }

    public DbSet<InfoHnc> InfoHncs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //    => optionsBuilder.UseSqlServer("Server=localhost;Database=hnc_db;Trusted_Connection=True; TrustServerCertificate=yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InfoHnc>(entity =>
        {
            entity.HasKey(e => e.IdInfo).HasName("PK__info_hnc__86741B5C22F578FE");

            entity.ToTable("info_hnc");

            entity.Property(e => e.IdInfo).HasColumnName("id_info");
            entity.Property(e => e.EstadoInfo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado_info");
            entity.Property(e => e.FechaInfo)
                .HasMaxLength(50)
                .HasColumnName("fecha_info");
            entity.Property(e => e.HoraInfo)
                .HasMaxLength(50)
                .HasColumnName("hora_info");
            entity.Property(e => e.MatriculaInfo)
                .HasMaxLength(50)
                .HasColumnName("matricula_info");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
