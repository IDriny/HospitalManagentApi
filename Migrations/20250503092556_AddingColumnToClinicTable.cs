using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagentApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingColumnToClinicTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Clinics");
        }
    }
}
