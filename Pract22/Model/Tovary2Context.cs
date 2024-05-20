using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pract22.Model;

public partial class Tovary2Context : DbContext
{
    public Tovary2Context()
    {
    }

    public Tovary2Context(DbContextOptions<Tovary2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<НаличиеТоваров> НаличиеТоваровs { get; set; }

    public virtual DbSet<Склады> Складыs { get; set; }

    public virtual DbSet<Товары> Товарыs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress; Database=Tovary2; User=исп-31; Password=1234567890; Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<НаличиеТоваров>(entity =>
        {
            entity.HasKey(e => new { e.НомерСклада, e.КодТовара });

            entity.ToTable("НаличиеТоваров");

            entity.HasOne(d => d.КодТовараNavigation).WithMany(p => p.НаличиеТоваровs)
                .HasForeignKey(d => d.КодТовара)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_НаличиеТоваров_Товары");

            entity.HasOne(d => d.НомерСкладаNavigation).WithMany(p => p.НаличиеТоваровs)
                .HasForeignKey(d => d.НомерСклада)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_НаличиеТоваров_Склады");
        });

        modelBuilder.Entity<Склады>(entity =>
        {
            entity.HasKey(e => e.Номер);

            entity.ToTable("Склады");

            entity.Property(e => e.Фиоруководителя).HasColumnName("ФИОРуководителя");
        });

        modelBuilder.Entity<Товары>(entity =>
        {
            entity.HasKey(e => e.Код);

            entity.ToTable("Товары");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
