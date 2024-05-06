using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PRN221_GroupProject.Models;

public partial class Prn221GroupProjectContext : DbContext
{
    public Prn221GroupProjectContext()
    {
    }

    public Prn221GroupProjectContext(DbContextOptions<Prn221GroupProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<CartHeader> CartHeaders { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<EmailSend> EmailSends { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-269H1O24;Initial Catalog=PRN221_GroupProject;Persist Security Info=True;User ID=sa;Password=12345;TrustServerCertificate=True;");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CartDetaill");

            entity.ToTable("CartDetail");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.CartDetail1)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("CartDetail");
            entity.Property(e => e.ProductId).HasMaxLength(36);
        });

        modelBuilder.Entity<CartHeader>(entity =>
        {
            entity.ToTable("CartHeader");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CartId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.ToTable("Coupon");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CouponId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        modelBuilder.Entity<EmailSend>(entity =>
        {
            entity.ToTable("EmailSend");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.EmailSendId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("emailSendId");
            entity.Property(e => e.Sendate)
                .HasColumnType("datetime")
                .HasColumnName("sendate");
            entity.Property(e => e.SenderId)
                .HasMaxLength(36)
                .HasColumnName("senderId");
            entity.Property(e => e.TemplateId)
                .HasMaxLength(36)
                .HasColumnName("templateId");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.ToTable("EmailTemplate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("createdBy");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.EmailTemplateId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("emailTemplateId");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(36)
                .HasColumnName("updatedBy");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updatedDate");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.ProductId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
