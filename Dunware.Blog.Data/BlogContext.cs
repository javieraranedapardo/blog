using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dunware.Blog.Data;

public partial class BlogContext : DbContext
{
    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Post> Post { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=blog;Username=postgres;Password=P1ssw4rd");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categoria_pkey");

            entity.ToTable("categoria");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("post_pkey");

            entity.ToTable("post");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoriaid).HasColumnName("categoriaid");
            entity.Property(e => e.Contenido).HasColumnName("contenido");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("slug");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");
            entity.Property(e => e.Ultimaactualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("ultimaactualizacion");
            entity.Property(e => e.Usuarioid).HasColumnName("usuarioid");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Post)
                .HasForeignKey(d => d.Categoriaid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("post_categoriaid_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Post)
                .HasForeignKey(d => d.Usuarioid)
                .HasConstraintName("post_usuarioid_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Email, "usuario_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("passwordHash");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
