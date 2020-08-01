using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookingClone.Data.Migrations
{
    public partial class FKImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_BookModel_BookModelId",
                table: "Image");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookModelId",
                table: "Image",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_BookModel_BookModelId",
                table: "Image",
                column: "BookModelId",
                principalTable: "BookModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_BookModel_BookModelId",
                table: "Image");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookModelId",
                table: "Image",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Image_BookModel_BookModelId",
                table: "Image",
                column: "BookModelId",
                principalTable: "BookModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
