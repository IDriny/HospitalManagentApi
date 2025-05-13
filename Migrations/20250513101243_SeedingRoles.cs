using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HospitalManagentApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctors_Clinics_ClinicId",
                table: "ClinicDoctors");

            migrationBuilder.DropIndex(
                name: "IX_ClinicDoctors_ClinicId",
                table: "ClinicDoctors");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "602c9c77-c088-47b2-b315-cd752ca1e791", null, "Administrator", "ADMINISTRATOR" },
                    { "664e4db0-f0ec-429a-84ce-cbd2e3d3e545", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctors_Clinics_DoctorId",
                table: "ClinicDoctors",
                column: "DoctorId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctors_Clinics_DoctorId",
                table: "ClinicDoctors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "602c9c77-c088-47b2-b315-cd752ca1e791");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "664e4db0-f0ec-429a-84ce-cbd2e3d3e545");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicDoctors_ClinicId",
                table: "ClinicDoctors",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctors_Clinics_ClinicId",
                table: "ClinicDoctors",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
