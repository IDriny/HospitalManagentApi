using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagentApi.Migrations
{
    /// <inheritdoc />
    public partial class fixingClinicsDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctors_Clinics_DoctorId",
                table: "ClinicDoctors");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctors_Doctor_DoctorId",
                table: "ClinicDoctors");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicDoctors_ClinicId",
                table: "ClinicDoctors",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctors_Clinics_ClinicId",
                table: "ClinicDoctors",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctors_Doctor_DoctorId",
                table: "ClinicDoctors",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctors_Clinics_ClinicId",
                table: "ClinicDoctors");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctors_Doctor_DoctorId",
                table: "ClinicDoctors");

            migrationBuilder.DropIndex(
                name: "IX_ClinicDoctors_ClinicId",
                table: "ClinicDoctors");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctors_Clinics_DoctorId",
                table: "ClinicDoctors",
                column: "DoctorId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctors_Doctor_DoctorId",
                table: "ClinicDoctors",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
