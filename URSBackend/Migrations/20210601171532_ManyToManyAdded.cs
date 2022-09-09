using Microsoft.EntityFrameworkCore.Migrations;

namespace URSBackend.Migrations
{
    public partial class ManyToManyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassroomStudent");

            migrationBuilder.CreateTable(
                name: "StudentsClassrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsClassrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsClassrooms_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsClassrooms_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsClassrooms_ClassroomId",
                table: "StudentsClassrooms",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsClassrooms_StudentId",
                table: "StudentsClassrooms",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsClassrooms");

            migrationBuilder.CreateTable(
                name: "ClassroomStudent",
                columns: table => new
                {
                    JoinedClassroomsId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassroomStudent", x => new { x.JoinedClassroomsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_ClassroomStudent_Classrooms_JoinedClassroomsId",
                        column: x => x.JoinedClassroomsId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassroomStudent_Users_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassroomStudent_StudentsId",
                table: "ClassroomStudent",
                column: "StudentsId");
        }
    }
}
