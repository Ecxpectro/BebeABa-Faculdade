﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB.Models
{
    public partial class BebeaBaContext : DbContext
    {
        public BebeaBaContext()
        {
        }

        public BebeaBaContext(DbContextOptions<BebeaBaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Children> Children { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Children>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.ChildrenFatherName).HasMaxLength(200);

                entity.Property(e => e.ChildrenMotherName).HasMaxLength(200);

                entity.Property(e => e.ChildrenName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ImgPath).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Children_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(220);

                entity.Property(e => e.UserFullName).IsRequired();

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}