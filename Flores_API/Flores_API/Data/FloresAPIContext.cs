using System;
using Flores_API.Models;
using Microsoft.EntityFrameworkCore;
namespace Flores_API.Data
{
    public partial class FloresAPIContext : DbContext
    {
        public FloresAPIContext()
        {
        }
        public FloresAPIContext(DbContextOptions<FloresAPIContext> options)
            : base(options)
        {
        }

        #region Context Prperties
        public virtual DbSet<TipoFlor> TipoFlores { get; set; } = null!;
        public virtual DbSet<DatosMedicion> DatosMedicion { get; set; } = null!;
        public virtual DbSet<Flores> Flores { get; set; } = null!;
        public virtual DbSet<Mediciones> Mediciones { get; set; } = null!;
        #endregion

        #region ContextDeclaration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_general_ci")
                .HasCharSet("latin1");

            #region TipoFlor
            modelBuilder.Entity<TipoFlor>(entity =>
            {
                entity.HasKey(e => e.IdTipoFlor)
                    .HasName("PRIMARY");


                entity.ToTable("tipo_flor");

                entity.Property(e => e.IdTipoFlor)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_tipo_flor");

                entity.Property(e => e.TipoFlorP)
                    .HasMaxLength(100)
                    .HasColumnName("tipo_flor");

                entity.Property(e => e.NombreEspecifico)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_especifico");

                entity.HasOne(d => d.IdFlorNavigation)
                    .WithOne(p => p.TipoFlorNavigation)
                    .HasForeignKey<Flores>(e => e.IdTipoFlor)
                    .HasConstraintName("flor_tipo_flor");

            });
            #endregion

            #region DatosMedicion
            modelBuilder.Entity<DatosMedicion>(entity =>
            {
                entity.HasKey(e => e.IDDato)
                    .HasName("PRIMARY");


                entity.ToTable("datos_medicion");

                entity.Property(e => e.IDDato)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_dato");

                entity.Property(e => e.NombreDato)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_dato");

                entity.Property(e => e.Parametros)
                    .HasMaxLength(200)
                    .HasColumnName("parametros");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(200)
                    .HasColumnName("observaciones");
            });
            #endregion

            #region Flores
            modelBuilder.Entity<Flores>(entity =>
            {
                entity.HasKey(e => e.IdFlor)
                    .HasName("PRIMARY");


                entity.ToTable("flores");

                entity.HasIndex(e => e.IdTipoFlor, "tipo_flor_flor");

                entity.Property(e => e.IdFlor)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_flor");

                entity.Property(e => e.IdTipoFlor)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_tipo_flor");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.FechaInsercion)
                    .HasMaxLength(11)
                    .HasColumnName("fecha_insercion");

                entity.HasOne(d => d.MedicionNavigation)
                    .WithOne(p => p.IdFlorNavigation)
                    .HasForeignKey<Mediciones>(e => e.IdFlor)
                    .HasConstraintName("medicion_flor_medicion");
            });
            #endregion

            #region Mediciones
            modelBuilder.Entity<Mediciones>(entity =>
            {
                entity.HasKey(e => e.IdMedicion)
                    .HasName("PRIMARY");


                entity.ToTable("mediciones");

                entity.HasIndex(e => e.IdFlor, "medicion_flor");
                entity.HasIndex(e => e.IdDatoMedicion, "medicion_tipo_medicion");

                entity.Property(e => e.IdMedicion)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_medicion");

                entity.Property(e => e.IdFlor)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_flor");

                entity.Property(e => e.IdDatoMedicion)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_dato_medicion");

                entity.Property(e => e.Valor)
                    .HasMaxLength(10)
                    .HasColumnName("valor");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(100)
                    .HasColumnName("comentario");

                entity.Property(e => e.Fecha)
                    .HasMaxLength(30)
                    .HasColumnName("fecha");

                entity.HasOne(d => d.IdDatoMedicionNavigation)
                    .WithOne(p => p.MedicionNavigation)
                    .HasForeignKey<DatosMedicion>(e => e.IDDato)
                    .HasConstraintName("medicion_flor");
            });
            #endregion

            OnModelCreatingPartial(modelBuilder);
            #endregion
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

