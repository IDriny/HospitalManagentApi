using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagentApi.Migrations
{
    /// <inheritdoc />
    public partial class EdittingTablesAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctor_Doctor_DoctorsId",
                table: "ClinicDoctor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicDoctor",
                table: "ClinicDoctor");

            migrationBuilder.RenameColumn(
                name: "DoctorsId",
                table: "ClinicDoctor",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicDoctor_DoctorsId",
                table: "ClinicDoctor",
                newName: "IX_ClinicDoctor_DoctorId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ClinicDoctor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicDoctor",
                table: "ClinicDoctor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicDoctor_ClinicId",
                table: "ClinicDoctor",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctor_Doctor_DoctorId",
                table: "ClinicDoctor",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicDoctor_Doctor_DoctorId",
                table: "ClinicDoctor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicDoctor",
                table: "ClinicDoctor");

            migrationBuilder.DropIndex(
                name: "IX_ClinicDoctor_ClinicId",
                table: "ClinicDoctor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClinicDoctor");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "ClinicDoctor",
                newName: "DoctorsId");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicDoctor_DoctorId",
                table: "ClinicDoctor",
                newName: "IX_ClinicDoctor_DoctorsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicDoctor",
                table: "ClinicDoctor",
                columns: new[] { "ClinicId", "DoctorsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicDoctor_Doctor_DoctorsId",
                table: "ClinicDoctor",
                column: "DoctorsId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
