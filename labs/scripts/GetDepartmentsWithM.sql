CREATE FUNCTION GetDepartmentsWithMoreThanGivenNumberOfEnrollments (NumEnrollments int)
RETURNS TABLE ("Id" integer,
	"Name" varchar,
	"Budget" decimal,
	"StartDate" date,
	"AdministratorId" integer)
AS $$
SELECT d."Id", d."Name", d."Budget", d."StartDate", d."AdministratorId"
  FROM "Departments" d
  INNER JOIN "Courses" c ON d."Id" = c."DepartmentId"
  INNER JOIN "Enrollments" e ON c."Id" = e."CourseId"
  GROUP BY d."Id", d."Name", d."Budget", d."StartDate", d."AdministratorId"
  HAVING COUNT(e."CourseId") > NumEnrollments
$$ LANGUAGE SQL;