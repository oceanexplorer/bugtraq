﻿// <auto-generated />
using System;
using BugTraq.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BugTraq.Api.Migrations
{
    [DbContext(typeof(BugTraqContext))]
    partial class BugTraqContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("BugTraq.Api.Models.Bug", b =>
                {
                    b.Property<int>("BugId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(300);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(75);

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("BugId");

                    b.HasIndex("UserId");

                    b.ToTable("Bugs");
                });

            modelBuilder.Entity("BugTraq.Api.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BugTraq.Api.Models.Bug", b =>
                {
                    b.HasOne("BugTraq.Api.Models.User", "User")
                        .WithMany("Bugs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
