﻿using System;
using System.Collections.Generic;
using Autenticacion.Api.Dominio.Persistencia.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Api.Dominio.Persistencia.DbContextMigraciones;

public partial class AutenticacionDbContext : DbContext
{
    public AutenticacionDbContext(DbContextOptions<AutenticacionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Indicativo> Indicativos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__D594664233FD1B2B");

            entity.Property(e => e.EstadoEliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaDeRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HoraDeRegistro).HasDefaultValueSql("(CONVERT([time],getdate()))");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personas");
        });

        modelBuilder.Entity<Indicativo>(entity =>
        {
            entity.HasKey(e => e.IdIndicativo).HasName("PK__Indicati__920347E94F4AA6CA");

            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Personas__2EC8D2AC5177E916");

            entity.Property(e => e.EstadoEliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaDeRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HoraDeRegistro).HasDefaultValueSql("(CONVERT([time],getdate()))");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.NombreFoto)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.IdIndicativoNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdIndicativo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personas_Indicativos");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584C0E7C8824");

            entity.Property(e => e.EsSuperUsuario).HasDefaultValue(true);
            entity.Property(e => e.EstadoEliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaDeRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HoraDeRegistro).HasDefaultValueSql("(CONVERT([time],getdate()))");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("UsuariosPkey");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A1906CF03A1").IsUnique();

            entity.Property(e => e.Contraseña).HasColumnType("text");
            entity.Property(e => e.Correo)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.EstadoEliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaDeRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HoraDeRegistro).HasDefaultValueSql("(CONVERT([time],getdate()))");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkPersonas");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkRoles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
