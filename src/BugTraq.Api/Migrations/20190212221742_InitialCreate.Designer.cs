﻿// <auto-generated />
using System;
using BugTraq.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BugTraq.Api.Migrations
{
    [DbContext(typeof(BugTraqContext))]
    [Migration("20190212221742_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("BugTraq.Api.Models.BugTraqContext+Bug", b =>
                {
                    b.Property<int>("BugId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("BugId");

                    b.HasIndex("UserId");

                    b.ToTable("Bugs");
                });

            modelBuilder.Entity("BugTraq.Api.Models.BugTraqContext+User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("Surname");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BugTraq.Api.Models.BugTraqContext+Bug", b =>
                {
                    b.HasOne("BugTraq.Api.Models.BugTraqContext+User", "User")
                        .WithMany("Bugs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
