CREATE FUNCTION GetEnrollmentsCountByDepartmentId (DepId integer) RETURNS integer
AS $$
SELECT COUNT(e."CourseId")
    FROM "Departments" d
    INNER JOIN "Courses" c ON c."DepartmentId" = d."Id"
    INNER JOIN "Enrollments" e ON c."Id" = e."CourseId"
    WHERE d."Id" = DepId
    GROUP BY d."Id"
$$ LANGUAGE SQL;