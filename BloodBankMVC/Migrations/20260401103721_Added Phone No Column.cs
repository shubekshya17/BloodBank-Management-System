using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodBankMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhoneNoColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Requestors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Requestors");
        }
    }
}
