# Lab 13 - SQL Queries

## Scenario

Execute SQL queries using `FromSql`, `SqlQuery`, and stored procedures in EF Core. The focus is on querying the database to retrieve and display specific information.

## Exercise 1: Show Best 5 Students

1. Write a method in `PersonService`, method `Best5Students`, to execute the following SQL query and display the results:

   <div class="flavor-sqlserver">

      ```sql
      SELECT TOP 5 p.Id, p.FirstName, p.LastName, p.Discriminator, p.HireDate, p.EnrollmentDate
      FROM People p
      INNER JOIN Enrollments e ON p.Id = e.StudentId
      WHERE p.Discriminator = 'Student'
      GROUP BY p.Id, p.FirstName, p.LastName, p.Discriminator, p.HireDate, p.EnrollmentDate
      HAVING AVG(e.Grade) IS NOT NULL
      ORDER BY AVG(e.Grade) DESC
      ```

   </div>
   <div class="flavor-pgsql">

      ```sql
      SELECT p."Id", p."FirstName", p."LastName", p."Discriminator", p."HireDate", p."EnrollmentDate"
      FROM school."People" p
      INNER JOIN school."Enrollments" e ON p."Id" = e."StudentId"
      WHERE p."Discriminator" = 'Student'
      GROUP BY p."Id", p."FirstName", p."LastName", p."Discriminator", p."HireDate", p."EnrollmentDate"
      HAVING AVG(e."Grade") IS NOT NULL
      ORDER BY AVG(e."Grade") DESC
      LIMIT 5
      ```

   </div>

1. Use `FromSql` or `FromSqlRaw` to execute this query against the `Students` DbSet.
1. Display the ID, first name, and last name of the top 5 students:

   ```txt
   15 - Arturo Anand
   21 - Robyn Suarez
   ...
   ```

## Exercise 2: Stored Procedure

1. Add logic to the `Seed` method to create this <div class="flavor-sqlserver"> stored procedure:

      ```sql
      CREATE PROCEDURE dbo.GetDepartmentsWithEnrollmentsAbove
      @NumberOfEnrollments INT
      AS
      BEGIN
         SELECT d.Id, d.Name, d.StartDate, d.Budget, d.AdministratorId
         FROM Departments d
         INNER JOIN Courses c ON d.Id = c.DepartmentId
         INNER JOIN Enrollments e ON c.Id = e.CourseId
         GROUP BY d.Id, d.Name, d.StartDate, d.Budget, d.AdministratorId
         HAVING COUNT(e.CourseId) > @NumberOfEnrollments
         ORDER BY d.Name DESC
      END;
      ```
   </div>
   <div class="flavor-pgsql"> function:

      ```sql
      CREATE OR REPLACE FUNCTION school."GetDepartmentsWithEnrollmentsAbove" (NumberOfEnrollments integer)
      RETURNS TABLE ("Id" integer,"Name" varchar(50), "StartDate" date, "Budget" decimal(8,2), "AdministratorId" integer)
      AS $$
         SELECT d."Id", d."Name", d."StartDate", d."Budget", d."AdministratorId"
         FROM school."Departments" d
         INNER JOIN school."Courses" c ON d."Id" = c."DepartmentId"
         INNER JOIN school."Enrollments" e ON c."Id" = e."CourseId"
         GROUP BY d."Id", d."Name", d."StartDate", d."Budget", d."AdministratorId"
         HAVING COUNT(e."CourseId") > NumberOfEnrollments
         ORDER BY d."Name" DESC
      $$ LANGUAGE SQL;
      ```

   </div>

1. Add logic in `DepartmentService`, method `GetDepartmentsWithEnrollmentsAbove` to invoke the <div class="flavor-sqlserver"> stored procedure. The stored procedure</div><div class="flavor-pgsql"> function. The function</div> accepts a parameter `NumberOfEnrollments` and returns departments with more than the specified number of enrollments.
1. Use `FromSql` or `FromSqlRaw` to execute the stored procedure with a parameter value of `5`.
1. Display the results:

   ```txt
   Show Departments with more than 5 enrollments:
     - Economics
     - Engineering
     - English
   ```

## Exercise 3: Show Students With Courses

(if time permits)

1. In a new folder 'ViewModels' create a class `StudentWithCourse`:

   ```cs
   public class StudentWithCourse
   {
       public string FirstName { get; set; } = null!;
       public string LastName { get; set; } = null!;
       public string Title { get; set; } = null!;
   }
   ```

1. In the `EnrollmentService`, method `DisplayStudentsAndCourses`, add logic to execute the following SQL query and map the results to the `StudentWithCourse` ViewModel:
   <div class="flavor-sqlserver">

      ```sql
      SELECT
      p.FirstName, p.LastName, c.Title
      FROM Enrollments AS e
      INNER JOIN Courses AS c
      ON e.CourseId = c.Id
      INNER JOIN People AS p
      ON e.StudentId = p.Id
      ORDER BY p.FirstName, c.Title
      ```

   </div>
   <div class="flavor-pgsql">

      ```sql
      SELECT
      p."FirstName", p."LastName", c."Title"
      FROM school."Enrollments" AS e
      INNER JOIN school."Courses" AS c
      ON e."CourseId" = c."Id"
      INNER JOIN school."People" AS p
      ON e."StudentId" = p."Id"
      ORDER BY p."FirstName", c."Title"
      ```

   </div>

1. Use `SqlQuery` or `SqlQueryRaw` to execute the query.
1. Display the results:

   ```txt
   Student: Alexandra Walker - Course: Macroeconomics
   Student: Alexandra Walker - Course: Microeconomics
   ...
   ```
