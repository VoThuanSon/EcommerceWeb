using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebClothes.Migrations
{
    /// <inheritdoc />
    public partial class unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "CustomerProfiles",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "CustomerProfiles",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
            migrationBuilder.CreateIndex(name: "IX_CustomerProfiles_Email", table: "CustomerProfiles", column: "Email", unique: true);
            migrationBuilder.CreateIndex(name: "IX_CustomerProfiles_Phone", table: "CustomerProfiles", column: "Phone", unique: true);
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
            migrationBuilder.DropIndex(name: "IX_CustomerProfiles_Email", table: "CustomerProfiles");
            migrationBuilder.DropIndex(name: "IX_CustomerProfiles_Phone", table: "CustomerProfiles");
        }
    }
}
