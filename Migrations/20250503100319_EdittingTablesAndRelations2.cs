using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagentApi.Migrations
{
    /// <inheritdoc />
    public partial class EdittingTablesAndRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctor_Clinics_ClinicId",
                table: "ClinicDoctor");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctor_Doctor_DoctorId",
                table: "ClinicDoctor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicDoctor",
                table: "ClinicDoctor");

            migrationBuilder.RenameTable(
                name: "ClinicDoctor",
                newName: "ClinicDoctors");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicDoctor_DoctorId",
                table: "ClinicDoctors",
                newName: "IX_ClinicDoctors_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicDoctor_ClinicId",
                table: "ClinicDoctors",
                newName: "IX_ClinicDoctors_ClinicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicDoctors",
                table: "ClinicDoctors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctors_Clinics_ClinicId",
                table: "ClinicDoctors",
                column: "ClinicId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctors_Clinics_ClinicId",
                table: "ClinicDoctors");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctors_Doctor_DoctorId",
                table: "ClinicDoctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicDoctors",
                table: "ClinicDoctors");

            migrationBuilder.RenameTable(
                name: "ClinicDoctors",
                newName: "ClinicDoctor");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicDoctors_DoctorId",
                table: "ClinicDoctor",
                newName: "IX_ClinicDoctor_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicDoctors_ClinicId",
                table: "ClinicDoctor",
                newName: "IX_ClinicDoctor_ClinicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicDoctor",
                table: "ClinicDoctor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctor_Clinics_ClinicId",
                table: "ClinicDoctor",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctor_Doctor_DoctorId",
                table: "ClinicDoctor",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
