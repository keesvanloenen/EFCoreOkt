using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Library.Migrations
{
    /// <inheritdoc />
    public partial class SQLCustomization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
           $"""
               CREATE OR REPLACE FUNCTION school."GetDepartmentsWithEnrollmentsAbove" (NumberOfEnrollments integer)
               RETURNS TABLE ("Id" integer, "Name" varchar(50), "StartDate" date, "Budget" decimal(8,2), "AdministratorId" integer)
               AS $$
                   SELECT d."Id", d."Name", d."StartDate", d."Budget", d."AdministratorId"
                   FROM school."Departments" d
                   INNER JOIN school."Courses" c ON d."Id" = c."DepartmentId"
                   INNER JOIN school."Enrollments" e ON c."Id" = e."CourseId"
                   GROUP BY d."Id", d."Name", d."StartDate", d."Budget", d."AdministratorId"
                   HAVING COUNT(e."CourseId") > NumberOfEnrollments
                   ORDER BY d."Name" DESC
               $$ LANGUAGE SQL;
            """

                );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            """
                DROP FUNCTION IF EXISTS school."GetDepartmentsWithEnrollmentsAbove"; 
            """
                );

        }
    }
}
