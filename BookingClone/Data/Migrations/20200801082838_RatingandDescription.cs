using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookingClone.Data.Migrations
{
    public partial class RatingandDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfferingDescription",
                table: "BookModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "BookModel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferingDescription",
                table: "BookModel");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "BookModel");
        }
    }
}
