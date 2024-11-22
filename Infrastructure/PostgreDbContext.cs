using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SalesScriptConstructor.Domain.Entities;

namespace SalesScriptConstructor.Infrastructure;

public partial class PostgreDbContext : DbContext
{
    public PostgreDbContext()
    {
    }

    public PostgreDbContext(DbContextOptions<PostgreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Block> Blocks { get; set; }

    public virtual DbSet<BlockConnection> BlockConnections { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Script> Scripts { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Block>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("blocks_pkey");

            entity.ToTable("blocks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.ScriptId)
                .ValueGeneratedOnAdd()
                .HasColumnName("script_id");

            entity.HasOne(d => d.Script).WithMany(p => p.Blocks)
                .HasForeignKey(d => d.ScriptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blocks_script_id_fkey");
        });

        modelBuilder.Entity<BlockConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("block_connections_pkey");

            entity.ToTable("block_connections");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NextBlockId)
                .ValueGeneratedOnAdd()
                .HasColumnName("next_block_id");
            entity.Property(e => e.PreviousBlockId)
                .ValueGeneratedOnAdd()
                .HasColumnName("previous_block_id");

            entity.HasOne(d => d.NextBlock).WithMany(p => p.BlockConnectionNextBlocks)
                .HasForeignKey(d => d.NextBlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("block_connections_next_block_id_fkey");

            entity.HasOne(d => d.PreviousBlock).WithMany(p => p.BlockConnectionPreviousBlocks)
                .HasForeignKey(d => d.PreviousBlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("block_connections_previous_block_id_fkey");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("managers_pkey");

            entity.ToTable("managers");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(255)
                .HasColumnName("hashed_password");
            entity.Property(e => e.Mail)
                .HasMaxLength(255)
                .HasColumnName("mail");
            entity.Property(e => e.Name)
                .HasMaxLength(35)
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(35)
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasMaxLength(35)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Script>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("scripts_pkey");

            entity.ToTable("scripts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatorId).HasColumnName("creator_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.Creator).WithMany(p => p.Scripts)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("scripts_creator_id_fkey");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sellers_pkey");

            entity.ToTable("sellers");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(255)
                .HasColumnName("hashed_password");
            entity.Property(e => e.Mail)
                .HasMaxLength(255)
                .HasColumnName("mail");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.Name)
                .HasMaxLength(35)
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(35)
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasMaxLength(35)
                .HasColumnName("surname");

            entity.HasOne(d => d.Manager).WithMany(p => p.Sellers)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("sellers_manager_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
