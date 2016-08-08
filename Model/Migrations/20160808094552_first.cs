using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Groups_Groups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IpCameras",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Cam_IpAddress = table.Column<string>(maxLength: 255, nullable: false),
                    Cam_Login = table.Column<string>(maxLength: 32, nullable: true),
                    Cam_Password = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpCameras", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Article = table.Column<string>(maxLength: 32, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    FullName = table.Column<string>(maxLength: 255, nullable: false),
                    GroupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Price = table.Column<decimal>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Goods_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goods_GroupId",
                table: "Goods",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ParentId",
                table: "Groups",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "IpCameras");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
