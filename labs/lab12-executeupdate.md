# Lab 12 - ExecuteUpdate

## Scenario

You will implement a feature to increase the credits of all courses in a selected department. Utilize the `ExecuteUpdate` method to efficiently update records in the database.

## Exercise 1: Implement Increase Credits Of Courses In Selected Department

1. In `CourseService`, method `IncreaseCreditsOfCoursesInSelectedDepartment`, prompt the user to enter the department ID:

   ```cs
   Console.Write("Provide Department Id: ");
   var departmentId = int.Parse(Console.ReadLine()!);
   ```

1. Implement functionality to increase the credits of all courses within that department by 10.
1. Use the `ExecuteUpdate` method to perform the update efficiently.
1. Show a confirmation message afterwards:

   ```txt
   Credits increased for 2 courses for department 1
   ```

1. If either the department doesn't exist or the department has no courses, show a message as well:

   ```txt
   No Courses found for department with id 5
   ```
