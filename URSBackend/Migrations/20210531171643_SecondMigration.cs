using Microsoft.EntityFrameworkCore.Migrations;

namespace URSBackend.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Teachers_InstructorId",
                table: "Classrooms");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_InstructorId",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "JoiningCode",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Classrooms");

            migrationBuilder.AddColumn<string>(
                name: "Instructor",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instructor",
                table: "Classrooms");

            migrationBuilder.AddColumn<string>(
                name: "JoiningCode",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Classrooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_InstructorId",
                table: "Classrooms",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Teachers_InstructorId",
                table: "Classrooms",
                column: "InstructorId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
