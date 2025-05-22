using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagentApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingSchduleColumnToDoctorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Schdule",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Schdule",
                table: "Doctor");
        }
    }
}
