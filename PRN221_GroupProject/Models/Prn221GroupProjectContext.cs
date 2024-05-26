﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PRN221_GroupProject.Models;

public partial class Prn221GroupProjectContext : IdentityDbContext<ApplicationUser>
{
    public Prn221GroupProjectContext()
    {
    }

    public Prn221GroupProjectContext(DbContextOptions<Prn221GroupProjectContext> options)
        : base(options)
    {
    }


    /*public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
    
    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }*/

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<CartHeader> CartHeaders { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<EmailSend> EmailSends { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-NAU21EPV;Initial Catalog=PRN221_GroupProject;User ID=sa;Password=12345;TrustServerCertificate=True;");
*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var admin = new IdentityRole("admin");
        admin.NormalizedName = "admin";
        var customer = new IdentityRole("customer");
        customer.NormalizedName = "customer";
        modelBuilder.Entity<IdentityRole>().HasData(admin, customer);


/*        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });




            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });


              entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
          });

          modelBuilder.Entity<AspNetUserToken>(entity =>
          {
              entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });


              entity.Property(e => e.LoginProvider).HasMaxLength(128);
              entity.Property(e => e.Name).HasMaxLength(128);

              entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);

          });

          });


        });
*/
        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CartDetaill");

            entity.ToTable("CartDetail");


            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.CartId).HasMaxLength(36);
            entity.Property(e => e.CartDetail1)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("CartDetail");
            entity.Property(e => e.CartId).HasMaxLength(36);
            entity.Property(e => e.ProductId).HasMaxLength(36);


            entity.HasOne(d => d.Cart).WithMany(p => p.CartDetails)
                .HasPrincipalKey(p => p.CartId)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartDetail_CartHeader");

            entity.HasOne(d => d.Product).WithMany(p => p.CartDetails)
                .HasPrincipalKey(p => p.ProductId)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartDetail_Product");
        });

        modelBuilder.Entity<CartHeader>(entity =>
        {
            entity.ToTable("CartHeader");

            entity.HasIndex(e => e.CartId, "IX_CartHeader").IsUnique();


            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CartId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
            entity.Property(e => e.CouponId).HasMaxLength(36);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Coupon).WithMany(p => p.CartHeaders)
                .HasPrincipalKey(p => p.CouponId)
                .HasForeignKey(d => d.CouponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartHeader_Coupon");

            entity.HasOne(d => d.User).WithMany(p => p.CartHeaders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartHeader_AspNetUsers");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.HasIndex(e => e.CategoryId, "IX_Category").IsUnique();

            entity.Property(e => e.CategoryId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Categories)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_AspNetUsers");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.ToTable("Coupon");


            entity.HasIndex(e => e.CouponId, "IX_Coupon").IsUnique();
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
                .HasMaxLength(450)
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

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EmailTemplates)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailTemplate_AspNetUsers");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Product_1");

            entity.ToTable("Product");

            entity.HasIndex(e => e.ProductId, "IX_Product").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.ProductId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_AspNetUsers");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("Product_Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasMaxLength(36);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.ProductCategoryId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(CONVERT([nvarchar](36),newid()))")
                .HasColumnName("Product_CategoryId");
            entity.Property(e => e.ProductId).HasMaxLength(36);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Updatedby).HasMaxLength(450);

            entity.HasOne(d => d.Category).WithMany(p => p.ProductCategories)
                .HasPrincipalKey(p => p.CategoryId)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category_Category");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category_AspNetUsers");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCategories)
                .HasPrincipalKey(p => p.ProductId)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category_Product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
