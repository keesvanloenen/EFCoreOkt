using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace School.Library.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "school");

            migrationBuilder.CreateTable(
                name: "People",
                schema: "school",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EnrollmentDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "school",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Budget = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AdministratorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_People_AdministratorId",
                        column: x => x.AdministratorId,
                        principalSchema: "school",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "school",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Credits = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "school",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoursesInstructors",
                schema: "school",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    InstructorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesInstructors", x => new { x.CourseId, x.InstructorId });
                    table.ForeignKey(
                        name: "FK_CoursesInstructors_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "school",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesInstructors_People_InstructorId",
                        column: x => x.InstructorId,
                        principalSchema: "school",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                schema: "school",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "integer", nullable: false),
                    StudentsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalSchema: "school",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_People_StudentsId",
                        column: x => x.StudentsId,
                        principalSchema: "school",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                schema: "school",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    Grade = table.Column<decimal>(type: "numeric(3,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "school",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_People_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "school",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnlineCourses",
                schema: "school",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnlineCourses_Courses_Id",
                        column: x => x.Id,
                        principalSchema: "school",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnsiteCourses",
                schema: "school",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnsiteCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnsiteCourses_Courses_Id",
                        column: x => x.Id,
                        principalSchema: "school",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "school",
                table: "People",
                columns: new[] { "Id", "Discriminator", "FirstName", "HireDate", "LastName" },
                values: new object[,]
                {
                    { 1, "Instructor", "Fadi", new DateOnly(2002, 8, 6), "Fakhouri" },
                    { 2, "Instructor", "Stacy", new DateOnly(1999, 6, 1), "Serrano" },
                    { 3, "Instructor", "Jasmine", new DateOnly(1997, 10, 12), "Stewart" },
                    { 4, "Instructor", "Kristen", new DateOnly(2001, 7, 23), "Xu" },
                    { 5, "Instructor", "Roger", new DateOnly(2000, 12, 7), "Van Houten" }
                });

            migrationBuilder.InsertData(
                schema: "school",
                table: "People",
                columns: new[] { "Id", "Discriminator", "EnrollmentDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 6, "Student", new DateOnly(2005, 9, 1), "Gytis", "Barzdukas" },
                    { 7, "Student", new DateOnly(2001, 9, 1), "Peggy", "Justice" },
                    { 8, "Student", new DateOnly(2002, 9, 1), "Yan", "Li" },
                    { 9, "Student", new DateOnly(2003, 9, 1), "Laura", "Norman" },
                    { 10, "Student", new DateOnly(2005, 9, 1), "Nino", "Olivotto" },
                    { 11, "Student", new DateOnly(2005, 9, 1), "Wayne", "Tang" },
                    { 12, "Student", new DateOnly(2002, 9, 1), "Meredith", "Alonso" },
                    { 13, "Student", new DateOnly(2004, 9, 1), "Sophia", "Lopez" },
                    { 14, "Student", new DateOnly(2000, 9, 1), "Meredith", "Browning" },
                    { 15, "Student", new DateOnly(2003, 9, 1), "Arturo", "Anand" },
                    { 16, "Student", new DateOnly(2000, 9, 1), "Alexandra", "Walker" },
                    { 17, "Student", new DateOnly(2004, 9, 1), "Carson", "Powell" },
                    { 18, "Student", new DateOnly(2001, 9, 1), "Damien", "Jai" },
                    { 19, "Student", new DateOnly(2005, 9, 1), "Robyn", "Carlson" },
                    { 20, "Student", new DateOnly(2001, 9, 1), "Carson", "Bryant" },
                    { 21, "Student", new DateOnly(2004, 9, 1), "Robyn", "Suarez" },
                    { 22, "Student", new DateOnly(2004, 9, 1), "Roger", "Holt" },
                    { 23, "Student", new DateOnly(2005, 9, 1), "Carson", "Alexander" },
                    { 24, "Student", new DateOnly(2001, 9, 1), "Isaiah", "Morgan" },
                    { 25, "Student", new DateOnly(2005, 9, 1), "Randall", "Martin" },
                    { 26, "Student", new DateOnly(2002, 9, 1), "Cody", "Rogers" },
                    { 27, "Student", new DateOnly(2001, 9, 1), "Anthony", "White" },
                    { 28, "Student", new DateOnly(2004, 9, 1), "Rachel", "Griffin" },
                    { 29, "Student", new DateOnly(2003, 9, 1), "Alicia", "Shan" },
                    { 30, "Student", new DateOnly(2003, 1, 30), "Erica", "Gao" }
                });

            migrationBuilder.InsertData(
                schema: "school",
                table: "People",
                columns: new[] { "Id", "Discriminator", "FirstName", "HireDate", "LastName" },
                values: new object[,]
                {
                    { 31, "DepartmentAdministrator", "Kim", new DateOnly(1995, 3, 11), "Abercrombie" },
                    { 32, "DepartmentAdministrator", "Roger", new DateOnly(1998, 7, 1), "Harui" },
                    { 33, "DepartmentAdministrator", "Roger", new DateOnly(2004, 2, 12), "Zheng" },
                    { 34, "DepartmentAdministrator", "Candace", new DateOnly(2001, 1, 15), "Kapoor" }
                });

            migrationBuilder.InsertData(
                schema: "school",
                table: "Departments",
                columns: new[] { "Id", "AdministratorId", "Budget", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, 31, 350000.0000m, "Engineering", new DateOnly(2007, 1, 9) },
                    { 2, 34, 120000.0000m, "English", new DateOnly(2007, 1, 9) },
                    { 3, 33, 200000.0000m, "Economics", new DateOnly(2007, 1, 9) },
                    { 4, 32, 250000.0000m, "Mathematics", new DateOnly(2007, 1, 9) }
                });

            migrationBuilder.InsertData(
                schema: "school",
                table: "Courses",
                columns: new[] { "Id", "Credits", "DepartmentId", "Title" },
                values: new object[,]
                {
                    { 1045, 4, 4, "Calculus" },
                    { 1050, 4, 1, "Chemistry" },
                    { 1061, 4, 1, "Physics" },
                    { 2021, 3, 2, "Composition" },
                    { 2030, 2, 2, "Poetry" },
                    { 2042, 4, 2, "Literature" },
                    { 3141, 4, 4, "Trigonometry" },
                    { 4022, 3, 3, "Microeconomics" },
                    { 4041, 3, 3, "Macroeconomics" },
                    { 4061, 2, 3, "Quantitative" }
                });

            migrationBuilder.InsertData(
                schema: "school",
                table: "Enrollments",
                columns: new[] { "CourseId", "StudentId", "Grade" },
                values: new object[,]
                {
                    { 2021, 6, 4.00m },
                    { 2030, 6, 3.50m },
                    { 2021, 7, 3.00m },
                    { 2030, 7, 4.00m },
                    { 2021, 8, 2.50m },
                    { 2042, 8, 3.50m },
                    { 2021, 9, 3.50m },
                    { 2042, 9, 4.00m },
                    { 2021, 10, 3.00m },
                    { 2042, 10, 3.00m },
                    { 4041, 11, 3.50m },
                    { 4041, 12, null },
                    { 4041, 13, 2.50m },
                    { 4041, 14, null },
                    { 4061, 14, null },
                    { 4022, 15, 4.00m },
                    { 4061, 15, 4.00m },
                    { 4022, 16, 3.00m },
                    { 4041, 16, 3.00m },
                    { 4022, 17, 2.50m },
                    { 4022, 18, 2.00m },
                    { 4022, 19, null },
                    { 4022, 20, 3.50m },
                    { 4061, 21, 4.00m },
                    { 4061, 22, 2.00m },
                    { 4022, 23, 3.00m },
                    { 4041, 23, 3.50m },
                    { 4061, 23, 2.50m },
                    { 1045, 24, 1.50m },
                    { 4022, 24, 3.00m },
                    { 1061, 25, 4.00m },
                    { 1050, 26, 3.50m },
                    { 1061, 26, 3.00m },
                    { 1045, 27, 2.50m },
                    { 1050, 27, 3.50m },
                    { 1061, 28, 4.00m },
                    { 1050, 29, 3.50m },
                    { 1061, 29, 4.00m }
                });

            migrationBuilder.InsertData(
                schema: "school",
                table: "OnlineCourses",
                columns: new[] { "Id", "Url" },
                values: new object[,]
                {
                    { 2021, "http://www.fineartschool.net/Composition" },
                    { 2030, "http://www.fineartschool.net/Poetry" },
                    { 3141, "http://www.fineartschool.net/Trigonometry" },
                    { 4041, "http://www.fineartschool.net/Macroeconomics" }
                });

            migrationBuilder.InsertData(
                schema: "school",
                table: "OnsiteCourses",
                columns: new[] { "Id", "Location", "Time" },
                values: new object[,]
                {
                    { 1045, "121 Smith", new TimeOnly(15, 30, 0) },
                    { 1050, "123 Smith", new TimeOnly(11, 30, 0) },
                    { 1061, "234 Smith", new TimeOnly(13, 15, 0) },
                    { 2042, "225 Adams", new TimeOnly(11, 0, 0) },
                    { 4022, "23 Williams", new TimeOnly(9, 0, 0) },
                    { 4061, "22 Williams", new TimeOnly(11, 15, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentId",
                schema: "school",
                table: "Courses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesInstructors_InstructorId",
                schema: "school",
                table: "CoursesInstructors",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentsId",
                schema: "school",
                table: "CourseStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_AdministratorId",
                schema: "school",
                table: "Departments",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                schema: "school",
                table: "Enrollments",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesInstructors",
                schema: "school");

            migrationBuilder.DropTable(
                name: "CourseStudent",
                schema: "school");

            migrationBuilder.DropTable(
                name: "Enrollments",
                schema: "school");

            migrationBuilder.DropTable(
                name: "OnlineCourses",
                schema: "school");

            migrationBuilder.DropTable(
                name: "OnsiteCourses",
                schema: "school");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "school");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "school");

            migrationBuilder.DropTable(
                name: "People",
                schema: "school");
        }
    }
}
