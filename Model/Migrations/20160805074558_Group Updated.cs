using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class GroupUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Groups_ParentID",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Groups_ParentId",
                table: "Groups",
                column: "ParentID",
                principalTable: "Groups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "ParentID",
                table: "Groups",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_ParentID",
                table: "Groups",
                newName: "IX_Groups_ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Groups_ParentId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Groups_ParentID",
                table: "Groups",
                column: "ParentId",
                principalTable: "Groups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Groups",
                newName: "ParentID");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_ParentId",
                table: "Groups",
                newName: "IX_Groups_ParentID");
        }
    }
}
