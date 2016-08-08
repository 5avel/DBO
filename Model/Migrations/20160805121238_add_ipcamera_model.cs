using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class add_ipcamera_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IpCameras");
        }
    }
}
