using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HospitalManagentApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangingColumnNameInPatientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "lName",
                table: "Patient",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "fName",
                table: "Patient",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Phone_Number",
                table: "Patient",
                newName: "PhoneNumber");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Patient",
                newName: "lName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Patient",
                newName: "fName");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Patient",
                newName: "Phone_Number");

            
        }
    }
}
