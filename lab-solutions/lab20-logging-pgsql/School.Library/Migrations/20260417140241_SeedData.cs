using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Library.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CoursesInstructors",
                columns: new[] { "CourseId", "InstructorId" },
                values: new object[,]
                {
           {2030, 1},
           {2021, 2},
           {1061, 3},
           {4041, 4},
           {4061, 5},
           {1050, 31},
           {1045, 32},
           {4022, 33},
           {2042, 34}
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CoursesInstructors",
                keyColumns: new[] { "CourseId", "InstructorId" },
                keyValues: new object[,]
                {
           { 2030, 1 },
           { 2021, 2 },
           { 1061, 3 },
           { 4041, 4 },
           { 4061, 5 },
           { 1050, 31 },
           { 1045, 32 },
           { 4022, 33 },
           { 2042, 34 }
                });

        }
    }
}
