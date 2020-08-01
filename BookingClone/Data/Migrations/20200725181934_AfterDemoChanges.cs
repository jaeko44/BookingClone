using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookingClone.Data.Migrations
{
    public partial class AfterDemoChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookModel_GuestDetails_GuestDetailsId",
                table: "BookModel");

            migrationBuilder.DropIndex(
                name: "IX_BookModel_GuestDetailsId",
                table: "BookModel");

            migrationBuilder.DropColumn(
                name: "GuestDetailsId",
                table: "BookModel");

            migrationBuilder.RenameColumn(
                name: "HotelConfNo",
                table: "BookModel",
                newName: "ValidUntil");

            migrationBuilder.RenameColumn(
                name: "CheckOutDate",
                table: "BookModel",
                newName: "ValidFrom");

            migrationBuilder.RenameColumn(
                name: "CheckInDate",
                table: "BookModel",
                newName: "HotelAddress");

            migrationBuilder.RenameColumn(
                name: "BookingDate",
                table: "BookModel",
                newName: "CreationDate");

            migrationBuilder.AlterColumn<int>(
                name: "NetRate",
                table: "BookModel",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "BookModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GrossRate",
                table: "BookModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumAdults",
                table: "BookModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumKids",
                table: "BookModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookModelId = table.Column<Guid>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_BookModel_BookModelId",
                        column: x => x.BookModelId,
                        principalTable: "BookModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_BookModelId",
                table: "Image",
                column: "BookModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "BookModel");

            migrationBuilder.DropColumn(
                name: "GrossRate",
                table: "BookModel");

            migrationBuilder.DropColumn(
                name: "MaximumAdults",
                table: "BookModel");

            migrationBuilder.DropColumn(
                name: "MaximumKids",
                table: "BookModel");

            migrationBuilder.RenameColumn(
                name: "ValidUntil",
                table: "BookModel",
                newName: "HotelConfNo");

            migrationBuilder.RenameColumn(
                name: "ValidFrom",
                table: "BookModel",
                newName: "CheckOutDate");

            migrationBuilder.RenameColumn(
                name: "HotelAddress",
                table: "BookModel",
                newName: "CheckInDate");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "BookModel",
                newName: "BookingDate");

            migrationBuilder.AlterColumn<string>(
                name: "NetRate",
                table: "BookModel",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<Guid>(
                name: "GuestDetailsId",
                table: "BookModel",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookModel_GuestDetailsId",
                table: "BookModel",
                column: "GuestDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookModel_GuestDetails_GuestDetailsId",
                table: "BookModel",
                column: "GuestDetailsId",
                principalTable: "GuestDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
