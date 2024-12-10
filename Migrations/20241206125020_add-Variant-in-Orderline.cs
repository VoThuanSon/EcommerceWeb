﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebClothes.Migrations
{
    /// <inheritdoc />
    public partial class addVariantinOrderline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Variant",
                table: "SaleOrderLines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Variant",
                table: "SaleOrderLines");
        }
    }
}
