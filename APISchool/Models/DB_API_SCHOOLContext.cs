using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APISchool.Models
{
    public partial class DB_API_SCHOOLContext : DbContext
    {
        public DB_API_SCHOOLContext()
        {
        }

        public DB_API_SCHOOLContext(DbContextOptions<DB_API_SCHOOLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.IdContact)
                    .HasName("PK__CONTACT__2AC556F637EF16D7");

                entity.ToTable("CONTACT");

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Relationship)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SecLastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.oCtoStudent)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.IdStudent)
                    .HasConstraintName("FK_StdntCtact");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.IdPayment)
                    .HasName("PK__PAYMENT__613289C05138670C");

                entity.ToTable("PAYMENT");

                entity.Property(e => e.Amount).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Document)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentDate).HasColumnType("date");

                entity.Property(e => e.RegistratedDate).HasColumnType("date");

                entity.Property(e => e.SecLastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.oPayStudent)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.IdStudent)
                    .HasConstraintName("FK_StdntPay");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK__ROLE__B43690540947EC96");

                entity.ToTable("ROLE");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStudent)
                    .HasName("PK__STUDENT__61B3510427C7A3CC");

                entity.ToTable("STUDENT");

                entity.Property(e => e.EnrolDate).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecLastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnsubscribeDate).HasColumnType("date");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__USERS__B7C926384DB2CA65");

                entity.ToTable("USERS");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecLastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
