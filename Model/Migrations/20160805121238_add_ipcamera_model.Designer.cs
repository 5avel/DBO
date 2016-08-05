using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Model;

namespace Model.Migrations
{
    [DbContext(typeof(DBODataContext))]
    [Migration("20160805121238_add_ipcamera_model")]
    partial class add_ipcamera_model
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("Model.DataModels.Good", b =>
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

            modelBuilder.Entity("Model.DataModels.Group", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.Property<int?>("ParentID");

                    b.HasKey("ID");

                    b.HasIndex("ParentID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Model.DataModels.IpCamera", b =>
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

            modelBuilder.Entity("Model.DataModels.Good", b =>
                {
                    b.HasOne("Model.DataModels.Group", "Group")
                        .WithMany("Goods")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("Model.DataModels.Group", b =>
                {
                    b.HasOne("Model.DataModels.Group", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentID");
                });
        }
    }
}
