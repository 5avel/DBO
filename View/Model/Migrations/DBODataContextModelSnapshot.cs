﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DBO.Model;

namespace DBO.Model.Migrations
{
    [DbContext(typeof(DBODataContext))]
    partial class DBODataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("DBO.Model.DataModel.Good", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Article")
                        .HasAnnotation("MaxLength", 32);

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<int?>("GroupId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.Property<decimal>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("ID");

                    b.HasIndex("GroupId");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("DBO.Model.DataModel.Group", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsExpanded");

                    b.Property<bool>("IsSelected");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.Property<int?>("ParentId");

                    b.HasKey("ID");

                    b.HasIndex("ParentId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DBO.Model.DataModel.IpCamera", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cam_IpAddress")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Cam_Login")
                        .HasAnnotation("MaxLength", 32);

                    b.Property<string>("Cam_Password")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.HasKey("ID");

                    b.ToTable("IpCameras");
                });

            modelBuilder.Entity("DBO.Model.DataModel.Good", b =>
                {
                    b.HasOne("DBO.Model.DataModel.Group", "Group")
                        .WithMany("Goods")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("DBO.Model.DataModel.Group", b =>
                {
                    b.HasOne("DBO.Model.DataModel.Group")
                        .WithMany("ChildrenGroups")
                        .HasForeignKey("ParentId");
                });
        }
    }
}
