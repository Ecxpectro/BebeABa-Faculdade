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
        public virtual DbSet<ChildrenTimeLine> ChildrenTimeLine { get; set; }
        public virtual DbSet<ForumAnswer> ForumAnswer { get; set; }
        public virtual DbSet<ForumRelation> ForumRelation { get; set; }
        public virtual DbSet<MainForum> MainForum { get; set; }
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

            modelBuilder.Entity<ChildrenTimeLine>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.FilePath).HasMaxLength(200);

                entity.Property(e => e.Height)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.TimeLineDate).HasColumnType("datetime");

                entity.Property(e => e.Vaccine).HasMaxLength(100);

                entity.Property(e => e.Weight)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Children)
                    .WithMany(p => p.ChildrenTimeLine)
                    .HasForeignKey(d => d.ChildrenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChildrenTimeLine_Children");
            });

            modelBuilder.Entity<ForumAnswer>(entity =>
            {
                entity.Property(e => e.ForumAnswer1)
                    .IsRequired()
                    .HasColumnName("ForumAnswer");

                entity.Property(e => e.ForumAnswerDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ForumAnswer)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ForumAnswer_Users");
            });

            modelBuilder.Entity<ForumRelation>(entity =>
            {
                entity.HasOne(d => d.ForumAnswer)
                    .WithMany(p => p.ForumRelation)
                    .HasForeignKey(d => d.ForumAnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ForumRelation_ForumAnswer");

                entity.HasOne(d => d.MainForum)
                    .WithMany(p => p.ForumRelation)
                    .HasForeignKey(d => d.MainForumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ForumRelation_MainForum");
            });

            modelBuilder.Entity<MainForum>(entity =>
            {
                entity.Property(e => e.MainForumDate).HasColumnType("datetime");

                entity.Property(e => e.MainForumMessage).IsRequired();

                entity.Property(e => e.MainForumTitle)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MainForum)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MainForum_Users");
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