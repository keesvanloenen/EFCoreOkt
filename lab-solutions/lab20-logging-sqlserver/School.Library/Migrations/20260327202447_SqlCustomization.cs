using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Library.Migrations
{
    /// <inheritdoc />
    public partial class SqlCustomization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
               CREATE PROCEDURE dbo.GetDepartmentsWithEnrollmentsAbove
                 @NumberOfEnrollments INT
               AS
               BEGIN
                 SELECT 
                   d.Id, d.Name, d.StartDate, d.Budget, d.AdministratorId
                 FROM Departments d
                 INNER JOIN Courses c ON d.Id = c.DepartmentId
                 INNER JOIN Enrollments e ON c.Id = e.CourseId
                 GROUP BY d.Id, d.Name, d.StartDate, d.Budget, d.AdministratorId
                 HAVING COUNT(e.CourseId) > @NumberOfEnrollments
                 ORDER BY d.Name
               END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE  dbo.GetDepartmentsWithEnrollmentsAbove");
        }
    }
}
