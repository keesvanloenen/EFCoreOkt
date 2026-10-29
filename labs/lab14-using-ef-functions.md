# Lab 14 - Using DB Functions

⭐ **This lab is optional** ⭐

## Scenario

You will be leveraging EF Core Functions that map to <div class="flavor-sqlserver"> SQL Server</div><div class="flavor-pgsql"> PostgreSQL</div>. Specifically, you will use `EF.Functions.StandardDeviationPopulation` to calculate the standard deviation of grades for each course.

## Exercise 1 - StandDeviationPopulation

_Standard deviation shows how much values in a data set typically differ from the mean. A small standard deviation means most values are close to the average; a large one means the values are more spread out._

_In this lab you'll compute the population standard deviation of grades for each course using `EF.Functions.StandardDeviationPopulation` so you can see how consistent (or variable) grading is within a course. If you are interested in the algorithm behind, see example below:_

```txt
- Grades: 80, 90
- Mean = (80 + 90) / 2 = 85
- Variance (population) = ((80−85)^2 + (90−85)^2) / 2 = (25 + 25) / 2 = 25
- Standard deviation = sqrt(25) = 5.0000
```

1. In `CourseService`, method `CourseGradeStandardDeviations`, implement the logic that calculates the standard deviation of grades for each course.

   The method should:
   - Use the `Enrollments` table to group grades by `CourseId`.
   - Use `EF.Functions.StandardDeviationPopulation` to calculate the standard deviation of grades within each group.
   - Filter out grades which are `null`.

1. The method should display the `CourseId` and the calculated standard deviation of grades for each course.
1. Format the standard deviation to four decimal places for better readability.

   ```txt
   CourseId: 1045 => 0,5000
   CourseId: 1050 => 0,0000
   ...
   ```
